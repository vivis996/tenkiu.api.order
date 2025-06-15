using tenkiu.api.order.Services.Db.SellOrderPaymentHistoryS;
using vm.common;

namespace tenkiu.api.order.App.PaymentHistoryApp;

public class SellOrderPaymentHistoryAppService(
  ISellOrderPaymentHistoryService service
) : DisposableBase, ISellOrderPaymentHistoryAppService
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
