using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.BuySellAllocation;

public class UpdateBuySellAllocationDto : BaseBuySellAllocationDto, IIdModel<int>
{
  /// <summary>
  /// Primary key of the allocation to update
  /// </summary>
  public int Id { get; set; }
}
