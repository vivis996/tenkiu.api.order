using tenkiu.api.order.Models.Dto.SellOrderDetail;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.SellOrder;

public class CreateSellOrderDto : BaseSellOrderDto, IDbCommon
{
  public IEnumerable<CreateSellOrderDetailDto> OrderDetails { get; set; }
}
