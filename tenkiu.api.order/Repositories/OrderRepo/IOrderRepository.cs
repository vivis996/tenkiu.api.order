using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.OrderRepo;

public interface IOrderRepository  : IRepository<Models.Entities.Order, int>
{
}
