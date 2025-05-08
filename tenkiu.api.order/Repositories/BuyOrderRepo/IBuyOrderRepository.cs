using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.BuyOrderRepo;

public interface IBuyOrderRepository  : IRepository<Models.Entities.BuyOrder, int>
{
}
