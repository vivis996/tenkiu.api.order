using tenkiu.api.order.Services.Db.RelationOrderStatusS;
using vm.common;

namespace tenkiu.api.order.App.RelationOrderStatusApp;

public class RelationOrderStatusAppService(
  IRelationOrderStatusService service
) : DisposableBase, IRelationOrderStatusAppService
{
  /// <summary>
  /// Disposes of resources managed by this service.
  /// </summary>
  protected override void DisposeResources()
  {
    // Dispose of the service to release unmanaged resources
    service.Dispose();
  }
}
