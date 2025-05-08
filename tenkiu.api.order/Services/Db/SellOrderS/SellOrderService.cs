using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models.Request;
using tenkiu.api.order.Repositories.OrderRepo;
using vm.common;
using vm.common.Utils;

namespace tenkiu.api.order.Services.Db.SellOrderS;

public class SellOrderService(
  ISellOrderRepository repository
) : DisposableBase, ISellOrderService
{
  public Task<SellOrder?> GetById(int id)
  {
    return repository.GetById(id, this.IncludeOrderDetails);
  }

  public Task<SellOrder?> GetByHash(string hash)
  {
    return repository.GetSingle(o => o.Hash == hash, this.IncludeOrderDetails);
  }

  public async Task<(IEnumerable<SellOrder>, int)> GetByRequestPagination(SellOrderSearchRequest request)
  {
    request.Order = this.GetOrderBy(request);
    var predicate = this.GetPredicate(request);

    var values = await repository.GetBySearchRequest<SellOrder, SellOrderSearchOrderBy>(request, predicate,
                                                                                this.IncludeOrderDetails);

    return (values, request.Count ?? 0);
  }

  public async Task<SellOrder> Create(SellOrder value)
  {
    var newValue = await repository.Create(value);
    newValue.Hash = newValue.Id.GetMd5();
    return await this.Update(newValue);
  }

  public async Task<SellOrder> Update(SellOrder value)
  {
    var @object = await repository.Update(value);
    return @object;
  }

  public async Task<SellOrder> Update(UpdateSellOrderDto value)
  {
    var @object = await repository.Update(value);
    return @object;
  }

  private Expression<Func<SellOrder, bool>> GetPredicate(SellOrderSearchRequest request)
  {
    return v =>
      (request.IdClient == null || v.IdClient == request.IdClient);
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
  }
}
