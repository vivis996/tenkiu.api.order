#!/bin/bash

# Function to handle errors
handle_error() {
    echo "Error: $1"
    exit 1
}

# Function to read a specific value from the environment file
read_value_from_env() {
    local env_file="$1"
    local key="$2"
    local value=$(awk -F '=' -v k="$key" '$1 == k {print $2}' "$env_file")
    if [[ -z "$value" ]]; then
        echo "Error: $key not found in $env_file"
        exit 1
    else
        echo "$value"
    fi
}

# Function to pull, tag, and remove Docker images
manage_docker_images() {
    local registry="$1"
    local repo="$2"
    local image_name="$3"
    local tag="$4"

    # Pull the Docker image
    docker pull "$registry/$repo/$image_name:$tag" || handle_error "Failed to pull Docker image: $repo/$image_name:$tag"

    # Tag the pulled image
    docker tag "$registry/$repo/$image_name:$tag" "$repo/$image_name:$tag" || handle_error "Failed to tag Docker image: $repo/$image_name:$tag"

    # Optionally, remove the old image
    docker image rm "$registry/$repo/$image_name:$tag" || handle_error "Failed to remove old Docker image: $repo/$image_name:$tag"
}

# Prompt for the environment file name
echo "Choose the environment file:"
echo "1. Production tenkiu.api.order"
read -p "Enter your choice (1): " ENV_CHOICE

# Set the environment file based on the product's choice
case $ENV_CHOICE in
    1)
        ENV_FILE="../.env/production.env"
        DOCKER_ENV_FILE="production.env"
        STACK_NAME="tenkiu-api-order"
        URL_API_HEALTH="https://tenkiu.shop/api/order/health"
        URL_REDIRECT="https://tenkiu.shop/api/order/health"
        ;;
    *)
        echo "Invalid choice. Using default environment file: ../.env/production.env"
        ENV_FILE="../.env/production.env"
        DOCKER_ENV_FILE="production.env"
        STACK_NAME="tenkiu-api-order"
        URL_API_HEALTH="https://tenkiu.shop/api/order/health"
        URL_REDIRECT="https://tenkiu.shop/api/order/health"
        ;;
esac

# Define the Docker registry parameter
MY_DOCKER_REGISTRY="192.168.0.106:5000"

# Define the repository and image names
REPO="vivisvivi"
IMAGE_API="tenkiu.api.order"

# Read the values of API_TAG from the chosen environment file
API_TAG=$(read_value_from_env "$ENV_FILE" "API_TAG")

# Echo the values of API_TAG
echo "--------------------------------"
echo "API_TAG value is: $API_TAG"
echo "--------------------------------"

# Pull, tag, and remove Docker images using the dynamic tags
manage_docker_images "$MY_DOCKER_REGISTRY" "$REPO" "$IMAGE_API" "$API_TAG"

# Define the pattern for the sed command to match everything after .env/
SED_PATTERN="s/\.\/\.env\/[^\/]*/\.\/\.env\/$DOCKER_ENV_FILE/g"

# Modify occurrences of ".env/..." to ".env/$DOCKER_ENV_FILE" in docker-compose.yaml
sed -i "" -e "$SED_PATTERN" ../docker-compose.yaml

# Run Docker Compose with environment variables and build
docker compose -f ../docker-compose.yaml --env-file "$ENV_FILE" -p "$STACK_NAME" up -d --build && \

# Log a message before waiting
echo "Waiting for 5 seconds before checking the API health..."

# Wait for 5 seconds
sleep 5

# Log another message
echo "Checking API health now..."

# Check API health
healthStatus=$(curl -s $URL_API_HEALTH)

# Determine the icon by status
if [[ "$healthStatus" == "Healthy" ]]; then
  statusMessage="white_check_mark"
else
  statusMessage="x"
fi

# Send a notification
curl -d "$STACK_NAME - $IMAGE_API up
$API_TAG version" -H "Title: Docker Deploy" https://ntfy.danielviveros.dev/dvrp-apps \
  -H "X-Tags: $statusMessage" \
  -H "Click: $URL_REDIRECT" \
  -H "Actions: view, View $API_TAG version, $URL_REDIRECT, clear=true;"