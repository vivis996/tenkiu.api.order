using tenkiu.api.order.Services.Db.SellOrderDetailS;
using vm.common;

namespace tenkiu.api.order.App.SellOrderDetailApp;

public class SellOrderDetailAppService(
  ISellOrderDetailService service
) : DisposableBase, ISellOrderDetailAppService
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
