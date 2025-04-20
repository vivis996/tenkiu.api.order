using my.template.api.Controllers.Handler;

namespace my.template.api.Controllers;

[AuthenticationJwtApi]
public abstract class ApiBaseController : vm.common.api.Base.ApiBaseController
{
  // No additional functionality needed, this class serves as a foundation
  // for controllers with JWT authentication.
}
