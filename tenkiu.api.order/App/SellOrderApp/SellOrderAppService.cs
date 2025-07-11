using AutoMapper;
using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;
using tenkiu.api.order.Services.Db.SellOrderDetailS;
using tenkiu.api.order.Services.Db.SellOrderPaymentHistoryS;
using tenkiu.api.order.Services.Db.SellOrderS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.App.SellOrderApp;

public class SellOrderAppService(
  ISellOrderService service,
  ISellOrderDetailService sellOrderDetailService,
  ISellOrderPaymentHistoryService sellOrderPaymentHistoryService,
  IMapper mapper
) : DisposableBase, ISellOrderAppService
{
  /// <summary>
  /// Retrieves an order by its unique identifier.
  /// </summary>
  /// <param name="id">The ID of the order to retrieve.</param>
  /// <returns>A response containing the order data if found, or a failure message if not.</returns>
  public async Task<BaseResponse<ResponseSellOrderDto?>> GetById(int id)
  {
    var value = await service.GetById(id);
    if (value is null)
      return new FailureResponse<ResponseSellOrderDto?>("Order not found");
    var order = mapper.Map<ResponseSellOrderDto>(value);
    await this.GetAndSetBalanceBySellOrderId(order);

    return new SuccessResponse<ResponseSellOrderDto?>(order);
  }

  /// <summary>
  /// Retrieves an order by its unique hash.
  /// </summary>
  /// <param name="hash">The hash of the order to retrieve.</param>
  /// <returns>A response containing the order data if found, or a failure message if not.</returns>
  public async Task<BaseResponse<ResponseSellOrderDto?>> GetByHash(string hash)
  {
    var value = await service.GetByHash(hash);
    if (value is null)
      return new FailureResponse<ResponseSellOrderDto?>("Order not found");
    var order = mapper.Map<ResponseSellOrderDto>(value);
    await this.GetAndSetBalanceBySellOrderId(order);

    return new SuccessResponse<ResponseSellOrderDto?>(order);
  }

  /// <summary>
  /// Searches for orders with pagination based on the provided criteria.
  /// </summary>
  /// <param name="searchRequest">The search and pagination parameters.</param>
  /// <returns>A response containing a paginated list of orders matching the criteria.</returns>
  public async Task<BaseResponse<PaginationResponse<ResponseSellOrderDto>>> GetByRequestPagination(SellOrderSearchRequest searchRequest)
  {
    searchRequest.PageSize ??= 10;
    searchRequest.PageNumber ??= 1;
    (var values, var count) = await service.GetByRequestPagination(searchRequest);
    var orders = mapper.Map<ResponseSellOrderDto[]>(values);
    await this.GetAndSetBalanceBySellOrderId(orders);
    return new SuccessResponse<PaginationResponse<ResponseSellOrderDto>>(new (orders)
    {
      Count = count,
      PageNumber = searchRequest.PageNumber,
      PageSize = searchRequest.PageSize,
    });
  }

  /// <summary>
  /// Creates a new order in the system.
  /// </summary>
  /// <param name="value">The order data to create.</param>
  /// <returns>A response containing the ID of the created order, or a failure message if creation fails.</returns>
  public async Task<BaseResponse<int>> Create(CreateSellOrderDto value)
  {
    var @object = await service.Create(value);
    if (@object.order is null)
      return new FailureResponse<int>(@object.message);
    return new SuccessResponse<int>(@object.order.Id);
  }

  /// <summary>
  /// Updates an existing order's data.
  /// </summary>
  /// <param name="value">The updated order data.</param>
  /// <returns>A response indicating whether the update was successful.</returns>
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
    var orderDetails = await sellOrderDetailService.UpdateAndDeleteNotListeItems(@object.Id, orderDetailDtos);

    return new SuccessResponse<bool>(@object is not null);
  }

  private async Task GetAndSetBalanceBySellOrderId(params ResponseSellOrderDto[] sellOrders)
  {
    var sellOrderIds = sellOrders.Select(x => x.Id).ToArray();
    var balancesRelation = await sellOrderPaymentHistoryService.GetBalanceBySellOrderId(sellOrderIds);
    foreach (var order in sellOrders)
    {
      // Try to get the list of balances; if none, fall back to TotalSellPrice
      balancesRelation.TryGetValue(order.Id, out var balances);
      order.Balances = balances;

      // Find the balance entry matching this order's currency
      var currencyEntry = balances?.FirstOrDefault(b => b.IdCurrency == order.BaseCurrencyId);

      // If we didn’t find one, or there were no balances at all, use the full price
      order.Balance = currencyEntry is null
        ? order.TotalSellPrice
        : order.TotalSellPrice - currencyEntry.Balance;
    }
  }
  
  /// <summary>
  /// Disposes of resources managed by this service.
  /// </summary>
  protected override void DisposeResources()
  {
    // Dispose of the service to release unmanaged resources
    service.Dispose();
    sellOrderDetailService.Dispose();
    sellOrderPaymentHistoryService.Dispose();
  }
}
