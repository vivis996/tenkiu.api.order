using tenkiu.api.order.Models.Dto.Order;
using tenkiu.api.order.Models.Request;
using vm.common.api.Models;

namespace tenkiu.api.order.App.OrderApp;

public interface IOrderAppService : IDisposable
{
  Task<BaseResponse<ResponseOrderDto?>> GetById(int id);
  Task<BaseResponse<ResponseOrderDto?>> GetByHash(string hash);
  Task<BaseResponse<PaginationResponse<ResponseOrderDto>>> GetByRequestPagination(OrderSearchRequest searchRequest);
  Task<BaseResponse<int>> Create(CreateOrderDto value);
  Task<BaseResponse<bool>> Update(UpdateOrderDto value);
}
