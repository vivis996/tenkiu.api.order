using tenkiu.api.order.Repositories.RelationOrderDetailsStatusRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.RelationOrderDetailsStatusS;

public class RelationOrderDetailsStatusService(
  IRelationOrderDetailsStatusRepository repository
) : DisposableBase, IRelationOrderDetailsStatusService
{
  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
