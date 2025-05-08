using tenkiu.api.order.Models.Dto.BuyOrder;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;

namespace tenkiu.api.order.Services.Db.BuyOrderS;

public interface IBuyOrderService : IDisposable
{
  Task<Models.Entities.BuyOrder?> GetById(int id);
  Task<(IEnumerable<Models.Entities.BuyOrder>, int)> GetByRequestPagination(BuyOrderSearchRequest request);
  Task<Models.Entities.BuyOrder> Create(BuyOrder value);
  Task<Models.Entities.BuyOrder> Update(UpdateBuyOrderDto value);
  Task<Models.Entities.BuyOrder> Update(Models.Entities.BuyOrder value);
}
