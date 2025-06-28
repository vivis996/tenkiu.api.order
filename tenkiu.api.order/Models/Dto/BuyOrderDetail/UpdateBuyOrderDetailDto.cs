using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.BuyOrderDetail;

public class UpdateBuyOrderDetailDto : BaseBuyOrderDetailDto, IIdModel<int>
{
  /// <summary>
  /// Primary key of the purchase line item to update
  /// </summary>
  public int Id { get; set; }
}
