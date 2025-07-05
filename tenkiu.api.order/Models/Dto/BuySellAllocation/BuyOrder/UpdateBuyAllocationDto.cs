using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.BuySellAllocation.BuyOrder;

public class UpdateBuyAllocationDto : BaseBuyAllocationDto, IIdModel<int>
{
  public int Id { get; set; }
}
