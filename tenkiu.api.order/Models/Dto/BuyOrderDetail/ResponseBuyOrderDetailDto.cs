using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.BuyOrderDetail;

public class ResponseBuyOrderDetailDto : BaseBuyOrderDetailDto, IIdModel<int>
{
  /// <summary>
  /// Primary key of the purchase line item
  /// </summary>
  public int Id { get; set; }
  public int PendingAllocation { get; set; }
}
