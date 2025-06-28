using tenkiu.api.order.Models.Dto.SellOrderDetail;
using tenkiu.api.order.Models.Entities;

namespace tenkiu.api.order.Services.Db.SellOrderDetailS;

public interface ISellOrderDetailService : IDisposable
{
  Task<IEnumerable<SellOrderDetail>> GetByOrderId(int orderId);
  Task<IEnumerable<int>> GetIdsByOrderId(int orderId);
  Task<IEnumerable<SellOrderDetail>> Create(int orderId, IEnumerable<CreateSellOrderDetailDto> orderDetailDtos);
  Task<IEnumerable<SellOrderDetail>> Update(int orderId, IEnumerable<UpdateSellOrderDetailDto> orderDetailDtos);
}
