using tenkiu.api.order.Models.Dto.BuyOrderDetail;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.BuyOrder;

public class CreateBuyOrderDto : BaseBuyOrderDto, IDbCommon
{
  public IEnumerable<CreateBuyOrderDetailDto> OrderDetails { get; set; }
}
