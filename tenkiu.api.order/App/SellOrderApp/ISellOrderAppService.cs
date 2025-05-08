using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Request;
using vm.common.api.Models;

namespace tenkiu.api.order.App.SellOrderApp;

public interface ISellOrderAppService : IDisposable
{
  Task<BaseResponse<ResponseSellOrderDto?>> GetById(int id);
  Task<BaseResponse<ResponseSellOrderDto?>> GetByHash(string hash);
  Task<BaseResponse<PaginationResponse<ResponseSellOrderDto>>> GetByRequestPagination(SellOrderSearchRequest searchRequest);
  Task<BaseResponse<int>> Create(CreateSellOrderDto value);
  Task<BaseResponse<bool>> Update(UpdateSellOrderDto value);
}
