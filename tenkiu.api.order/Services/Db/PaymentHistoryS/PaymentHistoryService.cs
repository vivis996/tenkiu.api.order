using tenkiu.api.order.Repositories.PaymentHistoryRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.PaymentHistoryS;

public class PaymentHistoryService(
  IPaymentHistoryRepository repository
) : DisposableBase, IPaymentHistoryService
{
  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
