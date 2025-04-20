using tenkiu.api.order.Controllers.Handler;

namespace tenkiu.api.order.Controllers;

[AuthenticationJwtApi]
public abstract class ApiBaseController : vm.common.api.Base.ApiBaseController
{
  // No additional functionality needed, this class serves as a foundation
  // for controllers with JWT authentication.
}
