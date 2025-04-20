using my.template.api.Models.Common;
using my.template.api.Models.Response;
using vm.common.api.Models;

namespace my.template.api.Services.CurrentUserService;

/// <summary>
/// Defines a service interface for managing the current user's authentication status, roles, and permissions.
/// </summary>
public interface ICurrentUserService : vm.common.Services.CurrentUserService.ICurrentUserService
{
  /// <summary>
  /// Gets the text key used to check if the user is authenticated.
  /// </summary>
  string IsAuthenticatedText { get; }

  /// <summary>
  /// Gets the text key used for the authenticated response.
  /// </summary>
  string AuthenticatedResponseText { get; }

  /// <summary>
  /// Asynchronously checks if the current user is authenticated.
  /// </summary>
  /// <returns>A Task that resolves to true if the user is authenticated; otherwise, false.</returns>
  Task<bool> IsUserAuthenticated();

  /// <summary>
  /// Sets the authentication status of the current user based on a validation response.
  /// </summary>
  /// <param name="validate">A response containing validation data for the user.</param>
  /// <returns>True if the user is authenticated; otherwise, false.</returns>
  bool SetCurrentAuthentication(BaseResponse<ValidateDto> validate);

  /// <summary>
  /// Asynchronously checks if the current user is the owner of the specified user ID.
  /// </summary>
  /// <param name="userId">The user ID to verify ownership against.</param>
  /// <returns>A Task that resolves to true if the user is the owner; otherwise, false.</returns>
  Task<bool> IsOwner(int userId);

  /// <summary>
  /// Determines whether the current user is an admin or the same user as the provided ID.
  /// </summary>
  /// <param name="id">The user ID to check against the current user.</param>
  /// <returns>True if the current user is authorized; otherwise, false.</returns>
  Task<bool> IsAdminOrSameUser(int userId);

  /// <summary>
  /// Checks if the current user has an admin role within the system.
  /// </summary>
  /// <returns>True if the user has an admin role; otherwise, false.</returns>
  bool IsAdminSystemRole();

  /// <summary>
  /// Retrieves the roles assigned to the current user.
  /// </summary>
  /// <returns>An IEnumerable of UserType representing the user's roles.</returns>
  IEnumerable<UserType> GetRoles();
}
