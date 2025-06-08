using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.OrderRepo;

public interface ISellOrderRepository  : IRepository<Models.Entities.SellOrder, int>
{
}
