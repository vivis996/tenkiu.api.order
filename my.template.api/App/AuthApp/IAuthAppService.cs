using my.template.api.Models.Response;
using vm.common.api.Models;

namespace my.template.api.App.AuthApp;

public interface IAuthAppService : IDisposable
{
  Task<BaseResponse<ValidateDto>> ValidateToken();
}
