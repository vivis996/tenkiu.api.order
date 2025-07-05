using tenkiu.api.order.Models.Dto.BuySellAllocation;
using vm.common.api.Models;

namespace tenkiu.api.order.App.BuySellAllocationApp;

public interface IBuySellAllocationAppService : IDisposable
{
  Task<BaseResponse<ResponseBuySellAllocationDto?>> GetById(int id);
  Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetBySellOrderId(int id);
  Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetByBuyOrderId(int id);
  Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetBySellOrderDetailId(int id);
  Task<BaseResponse<IEnumerable<ResponseBuyAllocationDto>>> GetByBuyOrderDetailId(int id);
  Task<BaseResponse<int>> Create(CreateBuySellAllocationDto value);
  Task<BaseResponse<bool>> Update(UpdateBuySellAllocationDto value);
  Task<BaseResponse<bool>> Delete(int id);
}
