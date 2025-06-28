using tenkiu.api.order.Models.Dto.BuyOrderDetail;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.BuyOrder;

public class ResponseBuyOrderDto : BaseBuyOrderDto, IIdModel<int>
{
  /// <summary>
  /// Primary key of the purchase order
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Collection of detail line items for this purchase order
  /// </summary>
  public IEnumerable<ResponseBuyOrderDetailDto> OrderDetails { get; set; }
}
