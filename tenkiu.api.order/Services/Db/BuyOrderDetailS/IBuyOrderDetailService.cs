using tenkiu.api.order.Models.Dto.BuyOrderDetail;
using tenkiu.api.order.Models.Entities;

namespace tenkiu.api.order.Services.Db.BuyOrderDetailS;

public interface IBuyOrderDetailService : IDisposable
{
  Task<IEnumerable<BuyOrderDetail>> GetByOrderId(int orderId);
  Task<IEnumerable<int>> GetIdsByOrderId(int orderId);
  Task<IEnumerable<BuyOrderDetail>> Create(int orderId, IEnumerable<CreateBuyOrderDetailDto> orderDetailDtos);
  Task<IEnumerable<BuyOrderDetail>> Update(int orderId, IEnumerable<UpdateBuyOrderDetailDto> orderDetailDtos);
}
