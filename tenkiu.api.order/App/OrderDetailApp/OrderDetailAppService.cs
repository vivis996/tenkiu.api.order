using tenkiu.api.order.Services.Db.OrderDetailS;
using vm.common;

namespace tenkiu.api.order.App.OrderDetailApp;

public class OrderDetailAppService(
  IOrderDetailService service
) : DisposableBase, IOrderDetailAppService
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
