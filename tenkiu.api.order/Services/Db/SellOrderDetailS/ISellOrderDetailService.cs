using tenkiu.api.order.Models.Dto.BuySellAllocation.BuyOrder;
using tenkiu.api.order.Models.Dto.SellOrderDetail;
using tenkiu.api.order.Models.Entities;

namespace tenkiu.api.order.Services.Db.SellOrderDetailS;

public interface ISellOrderDetailService : IDisposable
{
  Task<IEnumerable<SellOrderDetail>> GetByOrderId(int orderId);
  Task<SellOrderDetail?> GetByOrderId(int orderId, int productId, decimal sellPrice, int idCurrencySell);
  Task<IEnumerable<int>> GetIdsByOrderId(int orderId);
  Task<IEnumerable<SellOrderDetail>> Create(int orderId, IEnumerable<CreateSellOrderDetailDto> orderDetailDtos);
  Task<IEnumerable<SellOrderDetail>> Update(int orderId, IEnumerable<UpdateSellOrderDetailDto> orderDetailDtos);
  Task<IEnumerable<SellOrderDetail>> UpdateAndDeleteNotListeItems(int orderId, IEnumerable<UpdateSellOrderDetailDto> orderDetailDtos);
  Task Delete(IEnumerable<int> idsToDelete);
  Task<IEnumerable<SellOrderDetail>> UpsertOrderDetailsByQuantityAsync(int sellOrderId, BaseBuyAllocationDto value, int newQuantity);
  CreateSellOrderDetailDto NewSellOrderDetail(BaseBuyAllocationDto value, int newQuantity);
}
