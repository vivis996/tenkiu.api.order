using tenkiu.api.order.Services.Db.PaymentHistoryS;
using vm.common;

namespace tenkiu.api.order.App.PaymentHistoryApp;

public class PaymentHistoryAppService(
  IPaymentHistoryService service
) : DisposableBase, IPaymentHistoryAppService
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
