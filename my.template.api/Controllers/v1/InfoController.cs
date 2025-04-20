using Microsoft.AspNetCore.Mvc;
using my.template.api.App.InfoApp;
using vm.common.api.Models;

namespace my.template.api.Controllers.v1;

/// <summary>
/// Controller that provides version and deployment information for the API.
/// </summary>
/// <param name="service">The service to retrieve version and deployment information.</param>
[Route("api/mytemplate/v{version:apiVersion}/[controller]")]
[ApiController]
public class InfoController(
  IInfoAppService service
  ) : ApiBaseController
{
  /// <summary>
  /// Gets the current version and deployment information of the API.
  /// </summary>
  /// <returns>A Task representing the asynchronous operation, with a BaseResponse containing the version information.</returns>
  [HttpGet("version")]
  [ProducesResponseType(typeof(SuccessResponse<AppVersion>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<AppVersion>> GetVersion()
  {
    return await service.GetVersion();
  }

  /// <summary>
  /// Disposes resources used by the InfoController.
  /// </summary>
  protected override void DisposeResources()
  {
    service.Dispose();
  }
}
