using Microsoft.AspNetCore.Mvc;
using tenkiu.api.order.App.RelationOrderStatusApp;
using vm.common.api.Models;

namespace tenkiu.api.order.Controllers.v1;

[ApiController]
[Route("api/order/v{version:apiVersion}/[controller]")]
[Asp.Versioning.ApiVersion("1.0")]
public class RelationOrderStatusController(
  IRelationOrderStatusAppService service
) : ApiBaseController
{
  /// <summary>
  /// Disposes of resources managed by this controller.
  /// </summary>
  protected override void DisposeResources()
  {
    service.Dispose();
  }
}
