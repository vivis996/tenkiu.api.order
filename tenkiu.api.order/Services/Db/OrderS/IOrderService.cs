using tenkiu.api.order.Models.Dto.Order;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;

namespace tenkiu.api.order.Services.Db.OrderS;

public interface IOrderService : IDisposable
{
  Task<Models.Entities.Order?> GetById(int id);
  Task<Models.Entities.Order?> GetByHash(string hash);
  Task<(IEnumerable<Models.Entities.Order>, int)> GetByRequestPagination(OrderSearchRequest request);
  Task<Models.Entities.Order> Create(Order value);
  Task<Models.Entities.Order> Update(UpdateOrderDto value);
  Task<Models.Entities.Order> Update(Models.Entities.Order value);
}
