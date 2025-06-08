using AutoMapper;
using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;
using tenkiu.api.order.Services.Db.SellOrderS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.App.SellOrderApp;

public class SellOrderAppService(
  ISellOrderService service,
  IMapper mapper
) : DisposableBase, ISellOrderAppService
{
  public async Task<BaseResponse<ResponseSellOrderDto?>> GetById(int id)
  {
    var order = await service.GetById(id);
    if (order is null)
      return new FailureResponse<ResponseSellOrderDto?>("Order not found");

    return new SuccessResponse<ResponseSellOrderDto?>(mapper.Map<ResponseSellOrderDto>(order));
  }

  public async Task<BaseResponse<ResponseSellOrderDto?>> GetByHash(string hash)
  {
    var order = await service.GetByHash(hash);
    if (order is null)
      return new FailureResponse<ResponseSellOrderDto?>("Order not found");

    return new SuccessResponse<ResponseSellOrderDto?>(mapper.Map<ResponseSellOrderDto>(order));
  }

  public async Task<BaseResponse<PaginationResponse<ResponseSellOrderDto>>> GetByRequestPagination(SellOrderSearchRequest searchRequest)
  {
    searchRequest.PageSize ??= 10;
    searchRequest.PageNumber ??= 1;
    (var values, var count) = await service.GetByRequestPagination(searchRequest);
    return new SuccessResponse<PaginationResponse<ResponseSellOrderDto>>(new (mapper.Map<IEnumerable<ResponseSellOrderDto>>(values))
    {
      Count = count,
      PageNumber = searchRequest.PageNumber,
      PageSize = searchRequest.PageSize,
    });
  }

  public async Task<BaseResponse<int>> Create(CreateSellOrderDto value)
  {
    var order = mapper.Map<SellOrder>(value);
    order.Hash = string.Empty;
    var @object = await service.Create(order);
    return new SuccessResponse<int>(@object.Id);
  }

  public async Task<BaseResponse<bool>> Update(UpdateSellOrderDto value)
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
