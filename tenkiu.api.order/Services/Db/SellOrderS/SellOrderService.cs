using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;
using tenkiu.api.order.Repositories.OrderRepo;
using tenkiu.api.order.Services.Db.SellOrderDetailS;
using vm.common;
using vm.common.db.Models;
using vm.common.Services.CurrentUserService;
using vm.common.Utils;

namespace tenkiu.api.order.Services.Db.SellOrderS;

public class SellOrderService(
  IMapper mapper,
  ISellOrderDetailService sellOrderDetailService,
  ICurrentUserService currentUserService,
  ISellOrderRepository repository
) : DisposableBase, ISellOrderService
{
  public async Task<int?> GetIdByClientAndPeriod(int idClient, int idPeriod)
  {
    var id = await repository.GetDbSet().Where(o => o.IdClient == idClient && o.DeliveryPeriodId == idPeriod)
                     .Select(o => o.Id)
                     .FirstOrDefaultAsync();
    return id > 0 ? id : null;
  }

  public Task<SellOrder?> GetById(int id)
  {
    return repository.GetById(id, this.IncludeOrderDetails);
  }

  public Task<SellOrder?> GetByHash(string hash)
  {
    return repository.GetSingle(o => o.Hash == hash, this.IncludeOrderDetails);
  }

  public async Task<SellOrder?> GetProductOrderById(int sellOrderId, int idProduct)
  {
    var order = await repository.GetDbSet().Where(o => o.Id == sellOrderId &&
                                                     o.SellOrderDetails.Any(d => d.IdProduct == idProduct))
                                 .Include(o => o.SellOrderDetails)
                                 .Select(o => new SellOrder
                                 {
                                   Id = o.Id,
                                   Hash = o.Hash,
                                   SellOrderDetails = o.SellOrderDetails.Where(p => p.IdProduct == idProduct).Select(d => new SellOrderDetail
                                   {
                                     Id = d.Id,
                                     SellOrderId = d.SellOrderId,
                                     IdProduct = d.IdProduct,
                                     SellPrice = d.SellPrice,
                                     Quantity = d.Quantity,
                                     IdCurrencySell = d.IdCurrencySell,
                                   }).ToArray(),
                                 }).FirstOrDefaultAsync();
    return order;
  }

  public async Task<(IEnumerable<SellOrder>, int)> GetByRequestPagination(SellOrderSearchRequest request)
  {
    request.Order = this.GetOrderBy(request);
    var predicate = this.GetPredicate(request);

    var values = await repository.GetBySearchRequest<SellOrder, SellOrderSearchOrderBy>(request, predicate,
                                                                                this.IncludeOrderDetails);

    return (values, request.Count ?? 0);
  }

  public Task<bool> IsClientWithOneDeliveryPeriod(int clientId, int deliveryPeriodId)
  {
    return IsClientWithOneDeliveryPeriod(clientId, deliveryPeriodId, 0); 
  }

  public async Task<bool> IsClientWithOneDeliveryPeriod(int clientId, int deliveryPeriodId, int orderId)
  {
    var count = (await repository.GetList(o => o.IdClient == clientId && o.DeliveryPeriodId == deliveryPeriodId &&
                                               o.Id != orderId, o => 1, null)).Sum();
    return count > 0;
  }

  public async Task<SellOrder> Create(SellOrder value)
  {
    var newValue = await repository.Create(value);
    newValue.Hash = newValue.Id.GetMd5();
    return await this.Update(newValue);
  }

  public async Task<(SellOrder? order, string message)> Create(CreateSellOrderDto value)
  {
    var orderDetailDtos = value.OrderDetails ?? [];
    if (!orderDetailDtos.Any())
      return (null, "Order details cannot be empty");

    var isClientWithOneDeliveryPeriod = await this.IsClientWithOneDeliveryPeriod(value.IdClient, value.DeliveryPeriodId);
    if (isClientWithOneDeliveryPeriod)
      return (null, "Client already has an order with this delivery period");

    value.OrderDetails = [];
    var order = mapper.Map<SellOrder>(value);
    order.Hash = string.Empty;

    var @object = await this.Create(order);
    var orderDetails = await sellOrderDetailService.Create(@object.Id, orderDetailDtos);

    if (orderDetails is null || !orderDetails.Any() || orderDetails.Count() != orderDetailDtos.Count())
      return (null, "Failed to create order details");
    @object.SellOrderDetails = orderDetails.ToArray();

    return (@object, string.Empty);
  }

  public async Task<SellOrder> Update(SellOrder value)
  {
    var @object = await repository.Update(value);
    return @object;
  }

  public async Task<decimal> CaculateTotalSellPriceAndUpdate(int sellOrderId)
  {
    var totalSellPrice = await repository.GetById(sellOrderId, o => 
      o.SellOrderDetails.Where(d => d.IdCurrencySell == o.BaseCurrencyId)
                        .Select(d => d.SellPrice * d.Quantity).Sum(), null);
    await SetByParametersBatch(o => o.Id == sellOrderId,
                         s => s.SetProperty(e => e.TotalSellPrice, totalSellPrice));
    return totalSellPrice;
    
  }
  private async Task<Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>>> AddModifiedFields<T>(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
  {
    var currentUserId = await currentUserService.GetCurrentUserId();
    if (typeof(IDbModel<int>).IsAssignableFrom(typeof(T)))
    {
      setPropertyCalls = AppendSetProperty(setPropertyCalls,
                                           s => s
                                                .SetProperty(e => ((IDbModel<int>)e).ModifiedDt, DateTime.Now)
                                                .SetProperty(e => ((IDbModel<int>)e).ModifiedBy, currentUserId)
                                          );
    }
    return setPropertyCalls;
  }
  public async Task<int> SetByParametersBatch(
    Expression<Func<SellOrder, bool>> predicate,
    Expression<Func<SetPropertyCalls<SellOrder>, SetPropertyCalls<SellOrder>>> setPropertyCalls)
  {
    setPropertyCalls = await AddModifiedFields(setPropertyCalls);
    var updatedCount = await repository.GetDbSet()
                             .Where(predicate)
                             .ExecuteUpdateAsync(setPropertyCalls);

    return updatedCount;
  }
  private static Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> AppendSetProperty<TEntity>(
    Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> oldValue,
    Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> newValue)
  {
    var replace = new ReplacingExpressionVisitor(newValue.Parameters, [oldValue.Body]);
    var combined = replace.Visit(newValue.Body);
    return Expression.Lambda<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>>(combined, oldValue.Parameters);
  }
  public async Task<SellOrder> Update(UpdateSellOrderDto value)
  {
    var @object = await repository.Update(value);
    return @object;
  }

  private Expression<Func<SellOrder, bool>> GetPredicate(SellOrderSearchRequest request)
  {
    return v =>
      (request.IdClient == null || v.IdClient == request.IdClient) &&
      (request.DeliveryPeriodId == null || v.DeliveryPeriodId == request.DeliveryPeriodId) &&
      (request.DeliveryPeriod == null ||
       (v.DeliveryDate.ToDateTime(TimeOnly.MinValue).Date >= request.DeliveryPeriod.Start.Date &&
        v.DeliveryDate.ToDateTime(TimeOnly.MaxValue).Date <= request.DeliveryPeriod.End.Date));
  }

  private Expression<Func<SellOrder, object>>? GetOrderBy(SellOrderSearchRequest request)
  {
    return request.OrderBy switch
    {
      SellOrderSearchOrderBy.IdClient => p => p.IdClient,
      SellOrderSearchOrderBy.DtCreated => p => p.CreatedDt,
        SellOrderSearchOrderBy.None or
      _ => throw new ArgumentOutOfRangeException(nameof(request.OrderBy), request.OrderBy, "Invalid orderBy parameter"),
    };
  }
  
  private IIncludableQueryable<SellOrder, object> IncludeOrderDetails(IQueryable<SellOrder> query)
  {
    var includes = query.Include(p => p.SellOrderDetails);

    return includes;
  }

  protected override void DisposeResources()
  {
    repository.Dispose();
    sellOrderDetailService.Dispose();
    currentUserService.Dispose();
  }
}
