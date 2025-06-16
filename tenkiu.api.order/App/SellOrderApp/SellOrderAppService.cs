using AutoMapper;
using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;
using tenkiu.api.order.Services.Db.SellOrderDetailS;
using tenkiu.api.order.Services.Db.SellOrderS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.App.SellOrderApp;

public class SellOrderAppService(
  ISellOrderService service,
  ISellOrderDetailService sellOrderDetailService,
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
    var orderDetailDtos = value.OrderDetails ?? [];
    if (!orderDetailDtos.Any())
      return new FailureResponse<int>("Order details cannot be empty");
    var isClientWithOneDeliveryPeriod = await service.IsClientWithOneDeliveryPeriod(value.IdClient, value.DeliveryPeriodId);
    if (isClientWithOneDeliveryPeriod)
      return new FailureResponse<int>("Client already has an order with this delivery period");
    value.OrderDetails = [];
    var order = mapper.Map<SellOrder>(value);
    order.Hash = string.Empty;
    var @object = await service.Create(order);
    var orderDetails = await sellOrderDetailService.Create(order.Id, orderDetailDtos);
    if (orderDetails is null || !orderDetails.Any() || orderDetails.Count() != orderDetailDtos.Count())
      return new FailureResponse<int>("Failed to create order details");
    return new SuccessResponse<int>(@object.Id);
  }

  public async Task<BaseResponse<bool>> Update(UpdateSellOrderDto value)
  {
    var orderDetailDtos = value.OrderDetails ?? [];
    if (!orderDetailDtos.Any())
      return new FailureResponse<bool>("Order details cannot be empty");
    var isClientWithOneDeliveryPeriod = await service.IsClientWithOneDeliveryPeriod(value.IdClient, value.DeliveryPeriodId, value.Id);
    if (isClientWithOneDeliveryPeriod)
      return new FailureResponse<bool>("Client already has an order with this delivery period");
    value.OrderDetails = [];
    var @object = await service.Update(value);
    var orderDetails = await sellOrderDetailService.Update(@object.Id, orderDetailDtos);

    return new SuccessResponse<bool>(@object is not null);
  }
  
  /// <summary>
  /// Disposes of resources managed by this service.
  /// </summary>
  protected override void DisposeResources()
  {
    // Dispose of the service to release unmanaged resources
    service.Dispose();
    sellOrderDetailService.Dispose();
  }
}
