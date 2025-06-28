using AutoMapper;
using tenkiu.api.order.Models.Dto.BuyOrder;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;
using tenkiu.api.order.Services.Db.BuyOrderDetailS;
using tenkiu.api.order.Services.Db.BuyOrderS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.App.BuyOrderApp;

public class BuyOrderAppService(
  IBuyOrderService service,
  IBuyOrderDetailService buyOrderDetailService,
  IMapper mapper
) : DisposableBase, IBuyOrderAppService
{
  public async Task<BaseResponse<ResponseBuyOrderDto?>> GetById(int id)
  {
    var order = await service.GetById(id);
    if (order is null)
      return new FailureResponse<ResponseBuyOrderDto?>("Order not found");

    return new SuccessResponse<ResponseBuyOrderDto?>(mapper.Map<ResponseBuyOrderDto>(order));
  }

  public async Task<BaseResponse<PaginationResponse<ResponseBuyOrderDto>>> GetByRequestPagination(BuyOrderSearchRequest searchRequest)
  {
    searchRequest.PageSize ??= 10;
    searchRequest.PageNumber ??= 1;
    (var values, var count) = await service.GetByRequestPagination(searchRequest);
    return new SuccessResponse<PaginationResponse<ResponseBuyOrderDto>>(new (mapper.Map<IEnumerable<ResponseBuyOrderDto>>(values))
    {
      Count = count,
      PageNumber = searchRequest.PageNumber,
      PageSize = searchRequest.PageSize,
    });
  }

  public async Task<BaseResponse<int>> Create(CreateBuyOrderDto value)
  {
    var orderDetailDtos = value.OrderDetails ?? [];
    if (!orderDetailDtos.Any())
      return new FailureResponse<int>("Order details cannot be empty");
    value.OrderDetails = [];
    var order = mapper.Map<BuyOrder>(value);
    var @object = await service.Create(order);
    var orderDetails = await buyOrderDetailService.Create(order.Id, orderDetailDtos);
    if (orderDetails is null || !orderDetails.Any() || orderDetails.Count() != orderDetailDtos.Count())
      return new FailureResponse<int>("Failed to create order details");
    return new SuccessResponse<int>(@object.Id);
  }

  public async Task<BaseResponse<bool>> Update(UpdateBuyOrderDto value)
  {
    var orderDetailDtos = value.OrderDetails ?? [];
    if (!orderDetailDtos.Any())
      return new FailureResponse<bool>("Order details cannot be empty");
    value.OrderDetails = [];
    var @object = await service.Update(value);
    var orderDetails = await buyOrderDetailService.Update(@object.Id, orderDetailDtos);

    return new SuccessResponse<bool>(@object is not null);
  }
  
  /// <summary>
  /// Disposes of resources managed by this service.
  /// </summary>
  protected override void DisposeResources()
  {
    // Dispose of the service to release unmanaged resources
    service.Dispose();
    buyOrderDetailService.Dispose();
  }
}
