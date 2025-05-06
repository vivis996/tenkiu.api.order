using tenkiu.api.order.Services.Db.OrderS;
using vm.common;

namespace tenkiu.api.order.App.OrderApp;

public class OrderAppService(
  IOrderService service
) : DisposableBase, IOrderAppService
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
