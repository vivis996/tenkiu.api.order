using tenkiu.api.order.App.BuySellAllocationApp;
using tenkiu.api.order.Models.Dto.BuySellAllocation;
using tenkiu.api.order.Models.Dto.BuySellAllocation.BuyOrder;
using tenkiu.api.order.Models.Entities;
using vm.common.api.Models;

namespace tenkiu.api.order.Services.Db.BuySellAllocationS;

public interface IBuySellAllocationService : IDisposable
{
  Task<Models.Entities.BuySellAllocation?> GetById(int id);
  Task<IEnumerable<Models.Entities.BuySellAllocation>> GetBySellOrderId(int id);
  Task<IEnumerable<Models.Entities.BuySellAllocation>> GetByBuyOrderId(int id);
  Task<IEnumerable<Models.Entities.BuySellAllocation>> GetBySellOrderDetailId(int id);
  Task<IEnumerable<ClientSellAllocationSummaryDto>> GetBuyOrderDetailBySellOrderDetilId(IEnumerable<int> ids);
  Task<IEnumerable<ClientSellAllocationSummaryDto>> GetByBuyOrderDetailId(int id);
  Task<IEnumerable<ClientSellAllocationSummaryDto>> GetByBuyOrderDetailId(IEnumerable<int> ids);
  Task<IEnumerable<BuySellAllocation>> Create(IEnumerable<CreateBuySellAllocationDto> values);
  Task<(bool Success, string? Message, int SellOrderId)> CreateWithTransaction(CreateBuyAllocationDto value);
  Task<IEnumerable<BuySellAllocation>> Update(IEnumerable<UpdateBuySellAllocationDto> values);
  Task<IEnumerable<BuySellAllocation>> Update(IEnumerable<UpdateBuyAllocationDto> values);
  Task<(bool Success, string? Message)> UpdateWithTransaction(UpdateBuyAllocationDto value);
  Task<bool> Delete(int id);

  AllocationChangesDto DistributeAllocationsToSellOrderDetails(int buyOrderDetailId, int remainingQty, IEnumerable<SellOrderDetail> sellOrderDetails, Dictionary<int, AllocationMapEntry> allocationsMap);
  Task<(bool Success, string Message)> PersistAllocationChangesAsync(AllocationChangesDto allocationChangesDto);
}
