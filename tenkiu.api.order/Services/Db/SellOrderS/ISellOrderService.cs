using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;

namespace tenkiu.api.order.Services.Db.SellOrderS;

public interface ISellOrderService : IDisposable
{
  Task<int?> GetIdByClientAndPeriod(int idClient, int idPeriod);
  Task<Models.Entities.SellOrder?> GetById(int id);
  Task<Models.Entities.SellOrder?> GetByHash(string hash);
  Task<SellOrder?> GetProductOrderById(int sellOrderId, int idProduct);
  Task<(IEnumerable<Models.Entities.SellOrder>, int)> GetByRequestPagination(SellOrderSearchRequest request);
  Task<bool> IsClientWithOneDeliveryPeriod(int clientId, int deliveryPeriodId);
  Task<bool> IsClientWithOneDeliveryPeriod(int clientId, int deliveryPeriodId, int orderId);
  Task<Models.Entities.SellOrder> Create(SellOrder value);
  Task<(Models.Entities.SellOrder? order, string message)> Create(CreateSellOrderDto value);
  Task<Models.Entities.SellOrder> Update(UpdateSellOrderDto value);
  Task<Models.Entities.SellOrder> Update(Models.Entities.SellOrder value);
  Task<decimal> CaculateTotalSellPriceAndUpdate(int sellOrderId);
}
