using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using tenkiu.api.order.Models.Dto.BuyOrder;
using tenkiu.api.order.Models.Dto.BuyOrderDetail;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;
using tenkiu.api.order.Repositories.BuyOrderRepo;
using vm.common;
using vm.common.Utils;

namespace tenkiu.api.order.Services.Db.BuyOrderS;

public class BuyOrderService(
  IBuyOrderRepository repository
) : DisposableBase, IBuyOrderService
{
  public async Task<ResponseBuyOrderDto?> GetById(int id)
  {
    var order = await repository.GetDbSet()
                     .Where(o => o.Id == id).Select(this.Selector())
                     .FirstOrDefaultAsync();
    if (order is not null)
      AssignProperties(order);

    return order;
  }

  public async Task<(IEnumerable<ResponseBuyOrderDto>, int)> GetByRequestPagination(BuyOrderSearchRequest request)
  {
    request.Order = this.GetOrderBy(request);
    var predicate = this.GetPredicate(request);

    var values = await repository.GetBySearchRequest(request, predicate, this.Selector());
    foreach (var value in values)
    {
      AssignProperties(value);
    }

    return (values, request.Count ?? 0);
  }

  private Expression<Func<BuyOrder, ResponseBuyOrderDto>> Selector()
  {
    return o => new ResponseBuyOrderDto
    {
      Id = o.Id,
      PurchaseDate = o.PurchaseDate,
      IdStore = o.IdStore,
      BaseCurrencyId = o.BaseCurrencyId,
      ConvertedCurrencyId = o.ConvertedCurrencyId,
      ExchangeRate = o.ExchangeRate,
      OrderDetails = o.BuyOrderDetails.Select(d => new ResponseBuyOrderDetailDto
      {
        Id = d.Id,
        IdProduct = d.IdProduct,
        BuyOrderId = d.BuyOrderId,
        PurchasePrice = d.PurchasePrice,
        PurchasePriceTax = d.PurchasePriceTax,
        IdCurrencyPurchase = d.IdCurrencyPurchase,
        Quantity = d.Quantity,
        ConvertedPurchasePrice = d.ConvertedPurchasePrice,
        PendingAllocation = d.Quantity - d.BuySellAllocations.Sum(a => a.Quantity),
      }).ToArray(),
    };
  }

  public Task<BuyOrder> Create(BuyOrder value)
  {
    return repository.Create(value);
  }

  public async Task<BuyOrder> Update(BuyOrder value)
  {
    var @object = await repository.Update(value);
    return @object;
  }

  public async Task<BuyOrder> Update(UpdateBuyOrderDto value)
  {
    var @object = await repository.Update(value);
    return @object;
  }

  private Expression<Func<BuyOrder, bool>> GetPredicate(BuyOrderSearchRequest request)
  {
    // Compare DateOnly PurchaseDate by converting to DateTime
    return v =>
      (request.IdStore == null || v.IdStore == request.IdStore) &&
      (request.PurchasePeriod == null ||
        (v.PurchaseDate.ToDateTime(TimeOnly.MinValue).Date >= request.PurchasePeriod.Start.Date &&
           v.PurchaseDate.ToDateTime(TimeOnly.MaxValue).Date <= request.PurchasePeriod.End.Date));
  }

  private Expression<Func<BuyOrder, object>>? GetOrderBy(BuyOrderSearchRequest request)
  {
    return request.OrderBy switch
    {
      BuyOrderSearchOrderBy.PurchaseDate => p => p.PurchaseDate,
      BuyOrderSearchOrderBy.DtCreated => p => p.CreatedDt,
      BuyOrderSearchOrderBy.None or
      _ => throw new ArgumentOutOfRangeException(nameof(request.OrderBy), request.OrderBy, "Invalid orderBy parameter"),
    };
  }

  private static void AssignProperties(ResponseBuyOrderDto order)
  {
    order.TotalQuantity = order.OrderDetails.Sum(d => d.Quantity);
    order.PendingAllocation = order.OrderDetails.Sum(d => d.PendingAllocation);
  }

  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
