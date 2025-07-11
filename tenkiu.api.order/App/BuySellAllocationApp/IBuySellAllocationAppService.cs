using tenkiu.api.order.Models.Dto.BuySellAllocation;
using tenkiu.api.order.Models.Dto.BuySellAllocation.BuyOrder;
using vm.common.api.Models;

namespace tenkiu.api.order.App.BuySellAllocationApp;

public interface IBuySellAllocationAppService : IDisposable
{
  Task<BaseResponse<ResponseBuySellAllocationDto?>> GetById(int id);
  Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetBySellOrderId(int id);
  Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetByBuyOrderId(int id);
  Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetBySellOrderDetailId(int id);
  Task<BaseResponse<IEnumerable<ClientSellAllocationSummaryDto>>> GetByBuyOrderDetailId(int id);
  Task<BaseResponse<ClientPeriodProductAllocationDto>> GetByClientPeriodAndProductId(int idClient, int idPeriod, int idProduct);
  Task<BaseResponse<int>> Create(CreateBuyAllocationDto value);
  Task<BaseResponse<bool>> Update(UpdateBuyAllocationDto value);
  Task<BaseResponse<bool>> Delete(int id);
}
