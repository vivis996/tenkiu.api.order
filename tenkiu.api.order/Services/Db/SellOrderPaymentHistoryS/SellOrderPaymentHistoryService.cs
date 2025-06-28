using Microsoft.EntityFrameworkCore;
using tenkiu.api.order.Models;
using tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Repositories.SellOrderPaymentHistoryRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.SellOrderPaymentHistoryS;

public class SellOrderPaymentHistoryService(
  ISellOrderPaymentHistoryRepository repository
) : DisposableBase, ISellOrderPaymentHistoryService
{
  public Task<SellOrderPaymentHistory?> GetById(int id)
  {
    return repository.GetById(id);
  }

  public async Task<IEnumerable<SellOrderPaymentHistory>> GetBySellOrderId(int sellOrderId)
  {
    return await repository.GetList(x => x.SellOrderId == sellOrderId);
  }

  public Task<SellOrderPaymentHistory> Create(CreateSellOrderPaymentHistoryDto value)
  {
    return repository.Create(value);
  }

  public Task<SellOrderPaymentHistory> Update(UpdateSellOrderPaymentHistoryDto value)
  {
    return repository.Update(value);
  }

  public async Task<IEnumerable<PaymentDirectionRelation>> GetDirectionRelations()
  {
    return PaymentExtensions.GetDirectionRelations();
  }

  public async Task<IEnumerable<PaymentTypeDescription>> GetPaymentTypeDescriptions()
  {
    return PaymentExtensions.GetPaymentTypes();
  }

  public async Task<IEnumerable<BalanceDto>> GetBalanceBySellOrderId(int sellOrderId)
  {
    return (await this.GetBalanceBySellOrderId([sellOrderId,])).SelectMany(x => x.Value).ToArray();
  }

  public async Task<Dictionary<int, IEnumerable<BalanceDto>>> GetBalanceBySellOrderId(IEnumerable<int> sellOrderIds)
  {      
    var historyData = await repository.GetDbSet()
                                      .Where(p => sellOrderIds.Contains(p.SellOrderId))
                                      .GroupBy(p => new { p.SellOrderId, p.IdCurrency })
                                      .Select(g => new
                                      {
                                        g.Key.SellOrderId,
                                        g.Key.IdCurrency,
                                        Inflow = g.Sum(x => x.PaymentDirection == PaymentDirection.Inflow ? x.Amount : 0),
                                        Outflow = g.Sum(x => x.PaymentDirection == PaymentDirection.Outflow ? x.Amount : 0)
                                      })
                                      .ToListAsync();

    return historyData.GroupBy(x => x.SellOrderId)
                      .ToDictionary(
                                    grp => grp.Key,
                                    grp => grp.Select(item => new BalanceDto
                                    {
                                      IdCurrency = item.IdCurrency,
                                      Inflow = item.Inflow,
                                      Outflow = item.Outflow
                                    }).ToArray().AsEnumerable()
                                    );
  }

  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
