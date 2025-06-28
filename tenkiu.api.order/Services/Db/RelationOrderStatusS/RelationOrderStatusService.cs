using tenkiu.api.order.Repositories.RelationSellOrderStatusRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.RelationOrderStatusS;

public class RelationOrderStatusService(
  IRelationSellOrderStatusRepository repository
) : DisposableBase, IRelationOrderStatusService
{
  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
