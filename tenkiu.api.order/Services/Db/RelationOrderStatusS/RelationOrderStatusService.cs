using tenkiu.api.order.Repositories.RelationOrderStatusRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.RelationOrderStatusS;

public class RelationOrderStatusService(
  IRelationOrderStatusRepository repository
) : DisposableBase, IRelationOrderStatusService
{
  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
