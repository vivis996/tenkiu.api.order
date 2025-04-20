using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using my.template.api.App.AuthApp;
using my.template.api.Services.CurrentUserService;

namespace my.template.api.Controllers.Handler;

/// <summary>
/// Custom attribute to apply JWT authentication to a controller or action method.
/// This attribute uses the <see cref="AuthenticationJwtApiFilter"/> to check
/// if the current user is authenticated.
/// </summary>
public class AuthenticationJwtApiAttribute() : TypeFilterAttribute(typeof(AuthenticationJwtApiFilter));

/// <summary>
/// JWT authentication filter that checks if the user is authenticated.
/// This class uses the <see cref="ICurrentUserService"/> to validate user authentication.
/// </summary>
/// <param name="currentUserService">Service that checks the current user's authentication status.</param>
/// <param name="authAppService">Service to handle JWT token validation.</param>
public class AuthenticationJwtApiFilter(
  ICurrentUserService currentUserService,
  IAuthAppService authAppService
) : IAsyncAuthorizationFilter
{
  /// <summary>
  /// Initializes a new instance of the <see cref="AuthenticationJwtApiFilter"/> class.
  /// </summary>
  /// <summary>
  /// Asynchronously handles the authorization by checking if the user is authenticated.
  /// </summary>
  /// <param name="context">The context for the authorization filter.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
  {
    var validate = await authAppService.ValidateToken();
    currentUserService.SetCurrentAuthentication(validate);
  }
}
