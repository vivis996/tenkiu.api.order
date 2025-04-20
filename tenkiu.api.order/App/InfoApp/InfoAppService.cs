using vm.common;
using vm.common.api.Models;
using vm.common.api.Services.InfoS;

namespace tenkiu.api.order.App.InfoApp;

/// <summary>
/// Service that retrieves version and deployment information of the application.
/// </summary>
public class InfoAppService(
  IInfoService service
) : DisposableBase, IInfoAppService
{
  /// <summary>
  /// Retrieves version and deployment information, including the build version, deploy date, and environment.
  /// </summary>
  /// <returns>A Task representing the asynchronous operation, with a BaseResponse containing the version information.</returns>
  public async Task<BaseResponse<AppVersion>> GetVersion()
  {
    var version = await service.GetVersion();

    return new SuccessResponse<AppVersion>(version);
  }

  /// <summary>
  /// Disposes resources used by the InfoAppService.
  /// </summary>
  protected override void DisposeResources()
  {
    service.Dispose();
  }
}
