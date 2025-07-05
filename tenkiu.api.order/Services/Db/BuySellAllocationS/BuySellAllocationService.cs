using Microsoft.EntityFrameworkCore;
using tenkiu.api.order.Models.Dto.BuySellAllocation;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Repositories.BuySellAllocationRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.BuySellAllocationS;

public class BuySellAllocationService(
  IBuySellAllocationRepository repository
) : DisposableBase, IBuySellAllocationService
{
  public async Task<BuySellAllocation?> GetById(int id)
  {
    return await repository.GetById(id);
  }

  public async Task<IEnumerable<BuySellAllocation>> GetBySellOrderId(int id)
  {
    return await repository.GetList(v => v.SellOrderDetail.SellOrderId == id);
  }

  public async Task<IEnumerable<BuySellAllocation>> GetByBuyOrderId(int id)
  {
    return await repository.GetList(v => v.BuyOrderDetail.BuyOrderId == id);
  }

  public async Task<IEnumerable<BuySellAllocation>> GetBySellOrderDetailId(int id)
  {
    return await repository.GetList(v => v.SellOrderDetailId == id);
  }

  public async Task<IEnumerable<ResponseBuyAllocationDto>> GetByBuyOrderDetailId(int id)
  {
    // Project only needed fields to reduce data transfer
    var rawAllocations = await repository.GetDbSet()
                                         .Where(a => a.BuyOrderDetailId == id)
                                         .Select(a => new
                                         {
                                           a.Id,
                                           a.Quantity,
                                           a.BuyOrderDetailId,
                                           a.SellOrderDetailId,
                                           SellOrderId = a.SellOrderDetail.SellOrderId,
                                           DeliveryPeriodId = a.SellOrderDetail.SellOrder.DeliveryPeriodId,
                                           Hash = a.SellOrderDetail.SellOrder.Hash,
                                           IdClient = a.SellOrderDetail.SellOrder.IdClient,
                                           SellPrice = a.SellOrderDetail.SellPrice,
                                           IdCurrencySell = a.SellOrderDetail.IdCurrencySell,
                                         })
                                         .ToArrayAsync();
    // Group in memory into the DTO structure
    var result = rawAllocations
                 .GroupBy(x => x.IdClient)
                 .Select(g => new ResponseBuyAllocationDto
                 {
                   IdClient = g.Key,
                   TotalQuantity = g.Sum(x => x.Quantity),
                   Orders = g
                            .GroupBy(x => x.SellOrderId)
                            .Select(orderGroup => new ClientSellOrderAllocationDto
                            {
                              SellOrderId = orderGroup.Key,
                              DeliveryPeriodId = orderGroup.First().DeliveryPeriodId,
                              Hash = orderGroup.First().Hash,
                              Details = orderGroup.Select(x => new ClientSellOrderDetailAllocationDto
                              {
                                Id = x.Id,
                                BuyOrderDetailId = x.BuyOrderDetailId,
                                SellOrderDetailId = x.SellOrderDetailId,
                                Quantity = x.Quantity,
                                SellPrice = x.SellPrice,
                                IdCurrencySell = x.IdCurrencySell,
                              }).ToArray(),
                            })
                            .ToArray(),
                 }).ToArray();
    return result;
  }

  public Task<BuySellAllocation> Create(CreateBuySellAllocationDto value)
  {
    return repository.Create(value);
  }

  public Task<BuySellAllocation> Update(UpdateBuySellAllocationDto value)
  {
    return repository.Update(value);
  }

  public async Task<bool> Delete(int id)
  {
    return (await repository.DeleteById(id)) is not null;
  }

  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
