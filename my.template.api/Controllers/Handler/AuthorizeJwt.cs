using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using my.template.api.Models.Common;
using my.template.api.Models.Response;
using my.template.api.Services.CurrentUserService;
using vm.common.api.Models;

namespace my.template.api.Controllers.Handler;

/// <summary>
/// Custom attribute to enforce JWT authentication and role-based authorization on a controller or action.
/// This attribute requires that the user has one or more of the specified roles to access the resource.
/// </summary>
public class AuthorizeJwtAttribute : TypeFilterAttribute
{
  /// <summary>
  /// Initializes a new instance of the <see cref="AuthorizeJwtAttribute"/> class.
  /// </summary>
  /// <param name="requiredRoles">The roles required for the user to be authorized.</param>
  public AuthorizeJwtAttribute(params UserType[] requiredRoles) : base(typeof(AuthorizeJwt))
  {
    this.Arguments = [requiredRoles,];
  }
}

/// <summary>
/// JWT authorization filter that checks if the user is authenticated and has the required roles.
/// This filter is used to validate the JWT token and ensure that the user has access based on their roles.
/// </summary>
/// <param name="requiredRoles">Array of required roles to access the resource.</param>
/// <param name="currentUserService">Service for retrieving current user information and roles.</param>
public class AuthorizeJwt(
  UserType[] requiredRoles,
  ICurrentUserService currentUserService
) : IAsyncAuthorizationFilter
{
  /// <summary>
  /// Asynchronously handles the authorization by checking the user's authentication status and roles.
  /// </summary>
  /// <param name="context">The context for the authorization filter.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
  {
    // Check if the user is authenticated
    var valid = context.HttpContext.Items.TryGetValue(currentUserService.IsAuthenticatedText, out var value);
    if (!valid || value is false)
    {
      var errorMessage = "Authentication was unsuccessful";
      if (context.HttpContext.Items.TryGetValue(currentUserService.AuthenticatedResponseText, out var r)
          && r is BaseResponse<ValidateDto> validateDto)
      {
        errorMessage = GetMessage(validateDto);
      }
      SetUnauthorizedResult(context, new FailureResponse(errorMessage));

      return;
    }

    // Check if the user has the required roles
    if (this.ValidateRoles())
      return;

    const string message = "User does not have the required roles.";
    SetUnauthorizedResult(context, new FailureResponse(message));
  }

  /// <summary>
  /// Validates if the user has any of the required roles.
  /// </summary>
  /// <returns>True if the user has a required role, false otherwise.</returns>
  private bool ValidateRoles()
  {
    if (requiredRoles.Length == 0)
      return true;

    var roles = currentUserService.GetRoles().ToArray();
    return requiredRoles.Any(role => roles.Contains(role));
  }

  /// <summary>
  /// Extracts the error message from the validation response.
  /// </summary>
  /// <param name="validateDto">The validation response object.</param>
  /// <returns>The error message extracted from the response.</returns>
  private static string GetMessage(BaseResponse<ValidateDto> validateDto)
  {
    var message = "Error to validate token";
    if (validateDto.Data?.Message is not null)
      message = validateDto.Data.Message;
    else
    {
      if (validateDto is FailureResponse<ValidateDto> failureResponse)
        message = failureResponse.Message;
    }

    return message;
  }

  /// <summary>
  /// Sets the result of the context to unauthorized, with the specified failure response.
  /// </summary>
  /// <param name="context">The authorization filter context.</param>
  /// <param name="response">The failure response to set in the result.</param>
  private static void SetUnauthorizedResult(AuthorizationFilterContext context, FailureResponse response)
  {
    var result = new ObjectResult(response)
    {
      StatusCode = (int)HttpStatusCode.Unauthorized,
    };
    context.Result = result;
  }
}
