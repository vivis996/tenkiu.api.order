using my.template.api.Controllers.Handler;

namespace tenkiu.api.product.Controllers;

/// <summary>
/// Represents the base controller for the API.
/// This class is used to apply the <see cref="AuthenticationJwtApiAttribute"/> filter
/// to enforce JWT-based authentication across derived controllers.
/// </summary>
[AuthenticationJwtApi]
public abstract class BaseController : vm.common.api.Base.BaseController
{
  // No additional functionality needed, this class serves as a foundation
  // for controllers with JWT authentication.
}
