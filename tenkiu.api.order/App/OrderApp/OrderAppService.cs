using AutoMapper;
using tenkiu.api.order.Models.Dto.Order;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;
using tenkiu.api.order.Services.Db.OrderS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.App.OrderApp;

public class OrderAppService(
  IOrderService service,
  IMapper mapper
) : DisposableBase, IOrderAppService
{
  public async Task<BaseResponse<ResponseOrderDto?>> GetById(int id)
  {
    var order = await service.GetById(id);
    if (order is null)
      return new FailureResponse<ResponseOrderDto?>("Order not found");

    return new SuccessResponse<ResponseOrderDto?>(mapper.Map<ResponseOrderDto>(order));
  }

  public async Task<BaseResponse<ResponseOrderDto?>> GetByHash(string hash)
  {
    var order = await service.GetByHash(hash);
    if (order is null)
      return new FailureResponse<ResponseOrderDto?>("Order not found");

    return new SuccessResponse<ResponseOrderDto?>(mapper.Map<ResponseOrderDto>(order));
  }

  public async Task<BaseResponse<PaginationResponse<ResponseOrderDto>>> GetByRequestPagination(OrderSearchRequest searchRequest)
  {
    searchRequest.PageSize ??= 10;
    searchRequest.PageNumber ??= 1;
    (var values, var count) = await service.GetByRequestPagination(searchRequest);
    return new SuccessResponse<PaginationResponse<ResponseOrderDto>>(new (mapper.Map<IEnumerable<ResponseOrderDto>>(values))
    {
      Count = count,
      PageNumber = searchRequest.PageNumber,
      PageSize = searchRequest.PageSize,
    });
  }

  public async Task<BaseResponse<int>> Create(CreateOrderDto value)
  {
    var order = mapper.Map<Order>(value);
    order.Hash = string.Empty;
    var @object = await service.Create(order);
    return new SuccessResponse<int>(@object.Id);
  }

  public async Task<BaseResponse<bool>> Update(UpdateOrderDto value)
  {
    var @object = await service.Update(value);
    if (@object is null)
      return new FailureResponse<bool>("Order not found");

    return new SuccessResponse<bool>(true);
  }
  
  /// <summary>
  /// Disposes of resources managed by this service.
  /// </summary>
  protected override void DisposeResources()
  {
    // Dispose of the service to release unmanaged resources
    service.Dispose();
  }
}
