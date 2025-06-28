using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.RelationSellOrderStatusRepo;

public interface IRelationSellOrderStatusRepository  : IRepository<Models.Entities.SellOrderStatusRelation, int>
{
}
