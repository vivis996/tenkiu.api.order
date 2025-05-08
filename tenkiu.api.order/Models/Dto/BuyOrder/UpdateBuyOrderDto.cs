using tenkiu.api.order.Models.Dto.BuyOrderDetail;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.BuyOrder;

public class UpdateBuyOrderDto : BaseBuyOrderDto, IIdModel<int>
{
  /// <summary>
  /// Primary key of the purchase order to update
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Updated line items for this purchase order
  /// </summary>
  public IEnumerable<UpdateBuyOrderDetailDto> OrderDetails { get; set; }
}
