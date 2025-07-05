using tenkiu.api.order.Models.Dto.BuySellAllocation;

namespace tenkiu.api.order.Services.Db.BuySellAllocationS;

public interface IBuySellAllocationService : IDisposable
{
  Task<Models.Entities.BuySellAllocation?> GetById(int id);
  Task<IEnumerable<Models.Entities.BuySellAllocation>> GetBySellOrderId(int id);
  Task<IEnumerable<Models.Entities.BuySellAllocation>> GetByBuyOrderId(int id);
  Task<IEnumerable<Models.Entities.BuySellAllocation>> GetBySellOrderDetailId(int id);
  Task<IEnumerable<ResponseBuyAllocationDto>> GetByBuyOrderDetailId(int id);
  Task<Models.Entities.BuySellAllocation> Create(CreateBuySellAllocationDto value);
  Task<Models.Entities.BuySellAllocation> Update(UpdateBuySellAllocationDto value);
  Task<bool> Delete(int id);
}
