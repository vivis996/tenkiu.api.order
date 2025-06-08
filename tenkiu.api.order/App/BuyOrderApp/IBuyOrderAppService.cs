using tenkiu.api.order.Models.Dto.BuyOrder;
using tenkiu.api.order.Models.Request;
using vm.common.api.Models;

namespace tenkiu.api.order.App.BuyOrderApp;

public interface IBuyOrderAppService : IDisposable
{
  Task<BaseResponse<ResponseBuyOrderDto?>> GetById(int id);
  Task<BaseResponse<PaginationResponse<ResponseBuyOrderDto>>> GetByRequestPagination(BuyOrderSearchRequest searchRequest);
  Task<BaseResponse<int>> Create(CreateBuyOrderDto value);
  Task<BaseResponse<bool>> Update(UpdateBuyOrderDto value);
}
