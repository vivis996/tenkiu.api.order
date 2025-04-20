using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using my.template.api.Models.Common;
using my.template.api.Models.Response;
using vm.common;
using vm.common.api.Models;

namespace my.template.api.Services.CurrentUserService;

/// <summary>
/// Provides services related to the current user, including authentication checks, role verification, and retrieving user claims and roles from JWT tokens.
/// </summary>
public class CurrentUserService(
  IHttpContextAccessor httpContextAccessor
  ) : DisposableBase, ICurrentUserService
{
  /// <summary>
  /// Key for checking if the user is authenticated.
  /// </summary>
  public string IsAuthenticatedText => nameof(this.IsAuthenticatedText).Replace("Text", string.Empty);

  /// <summary>
  /// Key for the authenticated response text.
  /// </summary>
  public string AuthenticatedResponseText => nameof(this.AuthenticatedResponseText).Replace("Text", string.Empty);

  /// <summary>
  /// Checks if the current user is authenticated by retrieving the authentication status from the HTTP context.
  /// </summary>
  /// <returns>True if the user is authenticated; otherwise, false.</returns>
  public async Task<bool> IsUserAuthenticated()
  {
    var context = this.GetHttpContext();

    if (context.Items.TryGetValue(this.IsAuthenticatedText, out var value) && value is bool isAuthenticated)
    {
      return isAuthenticated;
    }

    throw new UnauthorizedAccessException("User cannot be identified");
  }

  /// <summary>
  /// Sets the current user's authentication status based on a validation response.
  /// </summary>
  /// <param name="validate">Validation response containing authentication data.</param>
  /// <returns>True if the user is authenticated; otherwise, false.</returns>
  public bool SetCurrentAuthentication(BaseResponse<ValidateDto> validate)
  {
    var context = this.GetHttpContext();
    var isAuthenticated = validate is { Data.IsValid: true, Success: true, };

    if (context is null)
      return isAuthenticated;

    context.Items[this.IsAuthenticatedText] = isAuthenticated;
    context.Items[this.AuthenticatedResponseText] = validate;

    if (isAuthenticated)
      context.User = this.GetClaimsPrincipalFromToken(this.GetToken());

    return isAuthenticated;
  }

  /// <summary>
  /// Checks if the current user is the owner of the specified user ID.
  /// </summary>
  /// <param name="userId">The user ID to check ownership against.</param>
  /// <returns>True if the user is the owner; otherwise, false.</returns>
  public async Task<bool> IsOwner(int userId)
  {
    try
    {
      if (!await this.IsUserAuthenticated())
        return false;

      var currentUserId = await this.GetCurrentUserId();
      return currentUserId == userId;
    }
    catch
    {
      return false;
    }
  }

  /// <summary>
  /// Determines whether the current user is an admin or the same user as the provided ID.
  /// </summary>
  /// <param name="userId">The user ID to check against the current user.</param>
  /// <returns>True if the current user is authorized; otherwise, false.</returns>
  public async Task<bool> IsAdminOrSameUser(int userId)
  {
    return this.IsAdminSystemRole() || await this.IsOwner(userId);
  }

  /// <summary>
  /// Checks if the current user has an admin role within the system.
  /// </summary>
  /// <returns>True if the user has an admin role; otherwise, false.</returns>
  public bool IsAdminSystemRole()
  {
    try
    {
      if (!this.IsUserAuthenticated().Result)
        return false;

      var roles = this.GetRoles();
      var adminRoles = new UserType[]
      {
        UserType.System,
        UserType.SuperUser,
        UserType.Admin,
      };

      return roles.Any(r => adminRoles.Contains(r));
    }
    catch
    {
      return false;
    }
  }

  /// <summary>
  /// Retrieves the current user's ID from claims or from a JWT token if not available in claims.
  /// </summary>
  /// <returns>The user ID if found; otherwise, throws an UnauthorizedAccessException.</returns>
  public async Task<int> GetCurrentUserId()
  {
    var userId = this.GetCurrentUserId(this.GetClaims());
    if (userId.HasValue)
      return userId.Value;

    var jwtToken = this.GetToken();

    if (string.IsNullOrEmpty(jwtToken))
      throw new UnauthorizedAccessException("JWT token is missing or invalid.");

    return this.ReadUserIdFromJwt();
  }

  /// <summary>
  /// Retrieves the JWT token from the HTTP request header.
  /// </summary>
  /// <returns>The JWT token as a string.</returns>
  private string GetToken()
  {
    return this.GetHttpContext()?.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last() ?? string.Empty;
  }

  /// <summary>
  /// Retrieves a ClaimsPrincipal object from the claims.
  /// </summary>
  /// <returns>A ClaimsPrincipal object containing the user's claims.</returns>
  private ClaimsPrincipal GetClaimsPrincipal()
  {
    var claims = this.GetClaims();
    var identity = new ClaimsIdentity(claims, "jwt");
    return new ClaimsPrincipal(identity);
  }

  /// <summary>
  /// Reads the user ID from the JWT token if it is available.
  /// </summary>
  /// <returns>The user ID if found; otherwise, throws an UnauthorizedAccessException.</returns>
  private int ReadUserIdFromJwt()
  {
    var claimsPrincipal = this.GetClaimsPrincipal();
    var userId = this.GetCurrentUserId(claimsPrincipal.Claims);
    if (userId.HasValue)
      return userId.Value;

    throw new UnauthorizedAccessException("Invalid user ID in JWT token.");
  }

  /// <summary>
  /// Retrieves the claims associated with the current HTTP context.
  /// </summary>
  /// <returns>An IEnumerable of Claim objects for the current user.</returns>
  private IEnumerable<Claim> GetClaims()
  {
    return this.GetHttpContext().User.Claims ?? [];
  }

  /// <summary>
  /// Retrieves the current HTTP context.
  /// </summary>
  /// <returns>The current HttpContext if available; otherwise, null.</returns>
  private HttpContext GetHttpContext()
  {
    return httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is not available.");
  }

  /// <summary>
  /// Retrieves the user ID from a collection of claims.
  /// </summary>
  /// <param name="claims">A collection of Claim objects.</param>
  /// <returns>The user ID if found; otherwise, null.</returns>
  private int? GetCurrentUserId(IEnumerable<Claim> claims)
  {
    var userIdClaim = claims.FirstOrDefault(c => c.Type == "nameid");
    return userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId) ? userId : (int?)null;
  }

  /// <summary>
  /// Retrieves the roles of the current user from claims.
  /// </summary>
  /// <returns>An IEnumerable of UserType values representing the user's roles.</returns>
  public IEnumerable<UserType> GetRoles()
  {
    return this.GetClaimsPrincipal()
        .FindAll("role")
        .Select(c => Enum.TryParse(typeof(UserType), c.Value, true, out var result)
                  ? (UserType?)result
                  : null)
        .Where(r => r.HasValue)
        .Select(r => r.Value)
        .ToArray();
  }

  /// <summary>
  /// Constructs a ClaimsPrincipal from a JWT token.
  /// </summary>
  /// <param name="token">The JWT token string.</param>
  /// <returns>A ClaimsPrincipal object containing the claims from the token.</returns>
  private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
  {
    var handler = new JwtSecurityTokenHandler();
    var jwtToken = handler.ReadJwtToken(token);
    var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
    return new ClaimsPrincipal(identity);
  }

  /// <summary>
  /// Disposes of any resources used by this service.
  /// </summary>
  protected override void DisposeResources()
  {
  }
}
