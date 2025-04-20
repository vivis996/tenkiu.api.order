using tenkiu.api.order.Models.Response;
using vm.common.api.Models;

namespace tenkiu.api.order.App.AuthApp;

public interface IAuthAppService : IDisposable
{
  Task<BaseResponse<ValidateDto>> ValidateToken();
}
