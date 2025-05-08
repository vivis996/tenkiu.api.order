using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using tenkiu.api.order.Models.Dto.Order;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;
using tenkiu.api.order.Repositories.OrderRepo;
using vm.common;
using vm.common.Utils;

namespace tenkiu.api.order.Services.Db.OrderS;

public class OrderService(
  IOrderRepository repository
) : DisposableBase, IOrderService
{
  public Task<Order?> GetById(int id)
  {
    return repository.GetById(id, this.IncludeOrderDetails);
  }

  public Task<Order?> GetByHash(string hash)
  {
    return repository.GetSingle(o => o.Hash == hash, this.IncludeOrderDetails);
  }

  public async Task<(IEnumerable<Order>, int)> GetByRequestPagination(OrderSearchRequest request)
  {
    request.Order = this.GetOrderBy(request);
    var predicate = this.GetPredicate(request);

    var values = await repository.GetBySearchRequest<Order, OrderSearchOrderBy>(request, predicate,
                                                                                this.IncludeOrderDetails);

    return (values, request.Count ?? 0);
  }

  public async Task<Order> Create(Order value)
  {
    var newValue = await repository.Create(value);
    newValue.Hash = newValue.Id.GetMd5();
    return await this.Update(newValue);
  }

  public async Task<Order> Update(Order value)
  {
    var @object = await repository.Update(value);
    return @object;
  }

  public async Task<Order> Update(UpdateOrderDto value)
  {
    var @object = await repository.Update(value);
    return @object;
  }

  private Expression<Func<Order, bool>> GetPredicate(OrderSearchRequest request)
  {
    return v =>
      (request.IdClient == null || v.IdClient == request.IdClient);
  }

  private Expression<Func<Order, object>>? GetOrderBy(OrderSearchRequest request)
  {
    return request.OrderBy switch
    {
      OrderSearchOrderBy.IdClient => p => p.IdClient,
      OrderSearchOrderBy.DtCreated => p => p.CreatedDt,
        OrderSearchOrderBy.None or
      _ => throw new ArgumentOutOfRangeException(nameof(request.OrderBy), request.OrderBy, "Invalid orderBy parameter"),
    };
  }
  
  private IIncludableQueryable<Order, object> IncludeOrderDetails(IQueryable<Order> query)
  {
    var includes = query.Include(p => p.OrderDetails);

    return includes;
  }

  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
