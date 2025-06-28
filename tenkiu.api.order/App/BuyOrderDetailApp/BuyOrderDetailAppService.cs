using tenkiu.api.order.Services.Db.BuyOrderDetailS;
using vm.common;

namespace tenkiu.api.order.App.BuyOrderDetailApp;

public class BuyOrderDetailAppService(
  IBuyOrderDetailService service
) : DisposableBase, IBuyOrderDetailAppService
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
