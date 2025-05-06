using tenkiu.api.order.Services.Db.RelationOrderDetailsStatusS;
using vm.common;

namespace tenkiu.api.order.App.RelationOrderDetailsStatusApp;

public class RelationOrderDetailsStatusAppService(
  IRelationOrderDetailsStatusService service
) : DisposableBase, IRelationOrderDetailsStatusAppService
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
