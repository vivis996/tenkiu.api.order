# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Add custom NuGet package source
# 192.168.0.106 -> dvrp2.local
RUN dotnet nuget add source "http://192.168.0.106:6100/v3/index.json" -n LocalNugets

COPY ["my.template.api/my.template.api.csproj", "my.template.api/"]
RUN dotnet restore "my.template.api/my.template.api.csproj"
COPY . .
WORKDIR "/src/my.template.api"
RUN dotnet build "my.template.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "my.template.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "my.template.api.dll"]
