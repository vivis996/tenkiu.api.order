using tenkiu.api.order.Repositories.OrderDetailRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.OrderDetailS;

public class OrderDetailService(
  IOrderDetailRepository repository
) : DisposableBase, IOrderDetailService
{
  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
