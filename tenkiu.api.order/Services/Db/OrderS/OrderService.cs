using tenkiu.api.order.Repositories.OrderRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.OrderS;

public class OrderService(
  IOrderRepository repository
) : DisposableBase, IOrderService
{
  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
