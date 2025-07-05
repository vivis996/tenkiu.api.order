using System.Linq.Expressions;
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tenkiu.api.order.App.BuySellAllocationApp;
using tenkiu.api.order.Models.Dto.BuySellAllocation;
using tenkiu.api.order.Models.Dto.BuySellAllocation.BuyOrder;
using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Dto.SellOrderDetail;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Repositories.BuySellAllocationRepo;
using tenkiu.api.order.Services.Db.SellOrderDetailS;
using tenkiu.api.order.Services.Db.SellOrderS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.Services.Db.BuySellAllocationS;

public class BuySellAllocationService(
  ISellOrderService sellOrderService,
  ISellOrderDetailService sellOrderDetailService,
  IBuySellAllocationRepository repository,
  IMapper mapper
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

  public async Task<IEnumerable<ClientSellAllocationSummaryDto>> GetBuyOrderDetailBySellOrderDetilId(IEnumerable<int> ids)
  {
    return await GetAllocationByPredicateAsync(a => ids.Contains(a.SellOrderDetailId));
  }

  public async Task<IEnumerable<ClientSellAllocationSummaryDto>> GetByBuyOrderDetailId(int id)
  {
    return await GetByBuyOrderDetailId([id,]);
  }

  public async Task<IEnumerable<ClientSellAllocationSummaryDto>> GetByBuyOrderDetailId(IEnumerable<int> ids)
  {
    return await GetAllocationByPredicateAsync(a => ids.Contains(a.BuyOrderDetailId));
  }

  public async Task<IEnumerable<BuySellAllocation>> Create(IEnumerable<CreateBuySellAllocationDto> values)
  {
    return await repository.Create(values);
  }

  public async Task<(bool Success, string? Message, int SellOrderId)> CreateWithTransaction(CreateBuyAllocationDto value)
  {
    var sellOrderId = await sellOrderService.GetIdByClientAndPeriod(value.IdClient, value.DeliveryPeriodId);

    using var scope = new TransactionScope(TransactionScopeOption.Required,
                                           new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted, },
                                           TransactionScopeAsyncFlowOption.Enabled);
    // If there's no sell order for the client in the specified period
    AllocationChangesDto allocationChangesDto;
    if (sellOrderId is null)
    {
      (var responseCreation, allocationChangesDto) = await this.CreateAllocationAndNewOrder(value);
      sellOrderId = responseCreation.Data;
    }
    else
    {
      allocationChangesDto = await this.CreateOrUpdateAllocationsIfOrderExist(value, sellOrderId.Value);
    }

    var response = await this.PersistAllocationChangesAsync(allocationChangesDto);
    if (!response.Success)
      return (false, response.Message, 0);
    scope.Complete();
    return (true, null, sellOrderId.Value);
  }

  public async Task<IEnumerable<BuySellAllocation>> Update(IEnumerable<UpdateBuySellAllocationDto> values)
  {
    return await repository.Update(values);
  }

  public async Task<IEnumerable<BuySellAllocation>> Update(IEnumerable<UpdateBuyAllocationDto> values)
  {
    return await repository.Update(values);
  }

  public async Task<(bool Success, string? Message)> UpdateWithTransaction(UpdateBuyAllocationDto value)
  {
    using var scope = new TransactionScope(TransactionScopeOption.Required,
                                           new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted, },
                                           TransactionScopeAsyncFlowOption.Enabled);
    var existingAllocation = await this.GetById(value.Id);
    if (existingAllocation is null)
      return (false, "Allocation not found");
    
    // Check new quantity against existing allocation
    var existingQuantity = existingAllocation.Quantity;
    // If quantity is unchanged, no need to update
    if (value.Quantity == existingQuantity)
      return (true, null);
    
    // If quantity is zero, not updating the allocation, this is a delete operation
    if (value.Quantity <= 0)
      return (false, "Quantity must be greater than zero to update allocation");
    
    // Retrieve and validate existing sell order
    var sellOrderId = await sellOrderService.GetIdByClientAndPeriod(value.IdClient, value.DeliveryPeriodId) 
                      ?? throw new InvalidOperationException("Sell order not found for the specified client and period.");
    // If quantity is greater than existing, we need to update the allocation
    if (value.Quantity > existingQuantity)
    {
      var neededAdditionalQty = value.Quantity - existingQuantity;
      value.Quantity = neededAdditionalQty;
      var record = await this.CreateOrUpdateAllocationsIfOrderExist(value, sellOrderId);
      var newAllocationResponse = await this.PersistAllocationChangesAsync(record);
      if (!newAllocationResponse.Success)
        return (false, newAllocationResponse.Message ?? "Failed to create or update allocation");
    }
    else
    {
      var sellOrder = await sellOrderService.GetProductOrderById(sellOrderId, value.ProductId)
                      ?? throw new InvalidOperationException($"Sell order {sellOrderId} does not contain product {value.ProductId}.");
      // If quantity is less than existing, we need to reduce the allocation and possibly delete the sell order detail
      var quantityToReduce = existingQuantity - value.Quantity;
      var sellOrderDetail = sellOrder.SellOrderDetails.FirstOrDefault(detail => detail.Id == existingAllocation.SellOrderDetailId);
      if (sellOrderDetail is null)
        return (false, "Sell order detail not found for the allocation");
      if (sellOrderDetail.Quantity < quantityToReduce)
        return (false, "Cannot reduce allocation more than available quantity in sell order detail");
      // Update the sell order detail quantity
      sellOrderDetail.Quantity -= quantityToReduce;
      if (sellOrderDetail.Quantity <= 0)
        throw new InvalidOperationException("Sell order detail quantity cannot be zero or negative after reduction");

      var update = mapper.Map<UpdateSellOrderDetailDto>(sellOrderDetail);
      await sellOrderDetailService.Update(sellOrderId, [update,]);

      var result = await this.Update([value,]);
      if (result is null)
        return (false, "Failed to update buy allocation");
      // Update total sell amount in the sell order
      // Calculate the new total sell price
      await sellOrderService.CaculateTotalSellPriceAndUpdate(sellOrderId);
    }
    scope.Complete();
    return (true, null);
  }

  public async Task<bool> Delete(int id)
  {
    return (await repository.DeleteById(id)) is not null;
  }

  private async Task<IEnumerable<ClientSellAllocationSummaryDto>> GetAllocationByPredicateAsync(Expression<Func<BuySellAllocation, bool>> predicate)
  {
    var rawAllocations = await repository.GetDbSet()
                                         .Where(predicate)
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

    return rawAllocations
           .GroupBy(x => x.IdClient)
           .Select(g => new ClientSellAllocationSummaryDto
           {
             IdClient = g.Key,
             TotalQuantity = g.Sum(x => x.Quantity),
             Orders = g
                      .GroupBy(x => x.SellOrderId)
                      .Select(orderGroup => new SellOrderAllocationDto
                      {
                        SellOrderId = orderGroup.Key,
                        DeliveryPeriodId = orderGroup.First().DeliveryPeriodId,
                        Hash = orderGroup.First().Hash,
                        Details = orderGroup.Select(x => new SellOrderDetailAllocationDto
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
  }

  public AllocationChangesDto DistributeAllocationsToSellOrderDetails(int buyOrderDetailId, int remainingQty, IEnumerable<SellOrderDetail> sellOrderDetails, Dictionary<int, AllocationMapEntry> allocationsMap)
  {
    var changesDto = new AllocationChangesDto([], []);
    foreach (var detail in sellOrderDetails.OrderBy(d => d.Id))
    {
      if (remainingQty <= 0) break;

      // Check for existing allocation and calculate already allocated quantity
      var isAllocationExists = allocationsMap.TryGetValue(detail.Id, out var allocationEntry);
      var allocatedQty = isAllocationExists ? allocationEntry.Quantity : 0;
      var pendingQty = detail.Quantity - allocatedQty;
      if (pendingQty <= 0) continue;

      var allocateQty = Math.Min(pendingQty, remainingQty);
      if (isAllocationExists)
      {
        changesDto.AllocationToUpdate.Add(new UpdateBuySellAllocationDto
        {
          Id = allocationEntry.Id,
          BuyOrderDetailId = buyOrderDetailId,
          SellOrderDetailId = detail.Id,
          Quantity = allocatedQty + allocateQty,
        });
      }
      else
      {
        changesDto.AllocationToCreate.Add(new CreateBuySellAllocationDto
        {
          BuyOrderDetailId = buyOrderDetailId,
          SellOrderDetailId = detail.Id,
          Quantity = allocateQty,
        });
      }

      remainingQty -= allocateQty;
    }
    return changesDto;
  }

  public async Task<(bool Success, string Message)> PersistAllocationChangesAsync(AllocationChangesDto allocationChangesDto)
  {
    var newAllocations = (await this.Create(allocationChangesDto.AllocationToCreate)).ToArray();
    if (newAllocations.Length != allocationChangesDto.AllocationToCreate.Count)
      return (false, "Failed to create buy allocation");
    var updatedAllocations = (await Update(allocationChangesDto.AllocationToUpdate)).ToArray();
    if (updatedAllocations.Length != allocationChangesDto.AllocationToUpdate.Count)
      return (false, "Failed to update buy allocation");
    return (true, "Allocations created and updated successfully");
  }

  private async Task<AllocationChangesDto> CreateOrUpdateAllocationsIfOrderExist(BaseBuyAllocationDto value, int sellOrderId)
  {
    // Retrieve and validate existing sell order
    var sellOrder = await sellOrderService.GetProductOrderById(sellOrderId, value.ProductId);

    // Work with a mutable list of order details
    var details = sellOrder?.SellOrderDetails ?? [];

    // Build a map of existing allocations
    var clientSummary = (await this.GetBuyOrderDetailBySellOrderDetilId(details.Select(d => d.Id))).FirstOrDefault();
    var allocationsMap = (clientSummary?.Orders ?? [])
                         .SelectMany(o => o.Details)
                         .Where(d => d.BuyOrderDetailId == value.BuyOrderDetailId)
                         .ToDictionary(d => d.SellOrderDetailId, d => new AllocationMapEntry(d.Id, d.Quantity));

    // Calculate total available quantity across details
    var totalAvailable = details.Sum(d => d.Quantity - (allocationsMap.TryGetValue(d.Id, out var entry) ? entry.Quantity : 0));

    // Determine additional quantity needed
    var neededAdditionalQty = Math.Max(0, value.Quantity - totalAvailable);
    if (neededAdditionalQty > 0)
    {
      // Create or update details to cover the shortfall
      var updatedDetails = await sellOrderDetailService.UpsertOrderDetailsByQuantityAsync(sellOrderId, value, neededAdditionalQty);
      await sellOrderService.CaculateTotalSellPriceAndUpdate(sellOrderId);
      // Merge new details into existing list
      details = details.Concat(updatedDetails).GroupBy(d => d.Id)
                          .Select(g => {
                            var det = g.First();
                            det.Quantity = g.Sum(x => x.Quantity);
                            return det;
                          }).ToArray();
    }

    // Distribute allocations across sorted details
    return this.DistributeAllocationsToSellOrderDetails(value.BuyOrderDetailId, value.Quantity, details, allocationsMap);
  }

  private async Task<(BaseResponse<int> response, AllocationChangesDto relation)> CreateAllocationAndNewOrder(CreateBuyAllocationDto value)
  {
    // Create a new sell order for the client in the specified period
    var newSellOrderDetail = sellOrderDetailService.NewSellOrderDetail(value, value.Quantity);
    var newSellOrder = new CreateSellOrderDto
    {
      IdClient = value.IdClient,
      DeliveryPeriodId = value.DeliveryPeriodId,
      BaseCurrencyId = value.IdCurrencySell ?? throw new InvalidOperationException("Base currency ID cannot be null"),
      TotalSellPrice = (value.Quantity * value.SellPrice) ?? throw new InvalidOperationException("Total sell price cannot be null"),
      OrderDetails = [newSellOrderDetail,],
    };
    var newSellOrderResult = await sellOrderService.Create(newSellOrder);
    if (newSellOrderResult.order is null)
    {
      var message = $"Failed to create sell order for the client in the specified period. {newSellOrderResult.message}.";
      throw new InvalidOperationException(message);
    }
    var orderDetailId = newSellOrderResult.order.SellOrderDetails.FirstOrDefault()?.Id ?? throw new InvalidOperationException("Sell order detail ID cannot be null");
    var newAllocation = new CreateBuySellAllocationDto
    {
      BuyOrderDetailId = value.BuyOrderDetailId,
      SellOrderDetailId = orderDetailId,
      Quantity = value.Quantity,
    };
    return (new SuccessResponse<int>(newSellOrderResult.order.Id), new AllocationChangesDto([newAllocation,], []));
  }

  protected override void DisposeResources()
  {
    sellOrderService.Dispose();
    sellOrderDetailService.Dispose();
    repository.Dispose();
  }
}
