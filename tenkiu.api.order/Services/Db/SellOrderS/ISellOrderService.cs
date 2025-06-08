using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;

namespace tenkiu.api.order.Services.Db.SellOrderS;

public interface ISellOrderService : IDisposable
{
  Task<Models.Entities.SellOrder?> GetById(int id);
  Task<Models.Entities.SellOrder?> GetByHash(string hash);
  Task<(IEnumerable<Models.Entities.SellOrder>, int)> GetByRequestPagination(SellOrderSearchRequest request);
  Task<Models.Entities.SellOrder> Create(SellOrder value);
  Task<Models.Entities.SellOrder> Update(UpdateSellOrderDto value);
  Task<Models.Entities.SellOrder> Update(Models.Entities.SellOrder value);
}
