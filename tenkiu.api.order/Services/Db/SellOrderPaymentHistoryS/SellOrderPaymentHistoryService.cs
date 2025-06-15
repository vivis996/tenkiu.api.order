using tenkiu.api.order.Repositories.SellOrderPaymentHistoryRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.SellOrderPaymentHistoryS;

public class SellOrderPaymentHistoryService(
  ISellOrderPaymentHistoryRepository repository
) : DisposableBase, ISellOrderPaymentHistoryService
{
  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
