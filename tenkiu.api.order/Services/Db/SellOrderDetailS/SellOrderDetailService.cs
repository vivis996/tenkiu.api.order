using tenkiu.api.order.Repositories.SellOrderDetailRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.SellOrderDetailS;

public class SellOrderDetailService(
  ISellOrderDetailRepository repository
) : DisposableBase, ISellOrderDetailService
{
  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
