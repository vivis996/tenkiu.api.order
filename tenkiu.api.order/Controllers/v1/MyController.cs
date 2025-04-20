using Microsoft.AspNetCore.Mvc;
using vm.common.api.Models;

namespace tenkiu.api.order.Controllers.v1;

[ApiController]
[Route("api/order/v{version:apiVersion}/[controller]")]
[Asp.Versioning.ApiVersion("1.0")]
public class MyController : ApiBaseController
{
  [HttpGet]
  [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<bool> Get()
  {
    return false;
  }
  
  protected override void DisposeResources()
  {
  }
}
