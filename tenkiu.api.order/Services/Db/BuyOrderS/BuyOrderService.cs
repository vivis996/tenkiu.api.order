using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using tenkiu.api.order.Models.Dto.BuyOrder;
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
  public Task<BuyOrder?> GetById(int id)
  {
    return repository.GetById(id, this.IncludeOrderDetails);
  }

  public async Task<(IEnumerable<BuyOrder>, int)> GetByRequestPagination(BuyOrderSearchRequest request)
  {
    request.Order = this.GetOrderBy(request);
    var predicate = this.GetPredicate(request);

    var values = await repository.GetBySearchRequest<BuyOrder, BuyOrderSearchOrderBy>(request, predicate,
                                                                                this.IncludeOrderDetails);

    return (values, request.Count ?? 0);
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
              (v.PurchaseDate.ToDateTime(TimeOnly.MinValue) >= request.PurchasePeriod.Start &&
               v.PurchaseDate.ToDateTime(TimeOnly.MaxValue) <= request.PurchasePeriod.End));
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
  
  private IIncludableQueryable<BuyOrder, object> IncludeOrderDetails(IQueryable<BuyOrder> query)
  {
    var includes = query.Include(p => p.BuyOrderDetails);

    return includes;
  }

  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
