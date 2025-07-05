using AutoMapper;
using tenkiu.api.order.Models.Dto.BuySellAllocation.BuyOrder;
using tenkiu.api.order.Models.Dto.SellOrderDetail;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Repositories.SellOrderDetailRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.SellOrderDetailS;

public class SellOrderDetailService(
  ISellOrderDetailRepository repository,
  IMapper mapper
) : DisposableBase, ISellOrderDetailService
{
  public async Task<IEnumerable<SellOrderDetail>> GetByOrderId(int orderId)
  {
    return await repository.GetList(o => o.SellOrderId == orderId);
  }

  public Task<SellOrderDetail?> GetByOrderId(int orderId, int productId, decimal sellPrice, int idCurrencySell)
  {
    return repository.GetSingle(detail => detail.SellOrderId == orderId &&
                                          detail.IdProduct == productId &&
                                          detail.SellPrice == sellPrice &&
                                          detail.IdCurrencySell == idCurrencySell);
  }

  public async Task<IEnumerable<int>> GetIdsByOrderId(int orderId)
  {
    return await repository.GetList(o => o.SellOrderId == orderId, o => o.Id, null);
  }

  public Task<IEnumerable<SellOrderDetail>> Create(int orderId, IEnumerable<CreateSellOrderDetailDto> orderDetailDtos)
  {
    if (orderDetailDtos == null || !orderDetailDtos.Any())
    {
      throw new ArgumentException("Order detail DTOs cannot be null or empty.", nameof(orderDetailDtos));
    }
    SetOrderId(orderId, orderDetailDtos);
    return repository.Create(orderDetailDtos);
  }

  public async Task<IEnumerable<SellOrderDetail>> Update(int orderId, IEnumerable<UpdateSellOrderDetailDto> orderDetailDtos)
  {
    var existingDetails = await this.GetIdsByOrderId(orderId);
    return await this.Update(orderId, orderDetailDtos, existingDetails);
  }

  public async Task<IEnumerable<SellOrderDetail>> UpdateAndDeleteNotListeItems(int orderId, IEnumerable<UpdateSellOrderDetailDto> orderDetailDtos)
  {
    var existingDetails = await this.GetIdsByOrderId(orderId);
    // Get array of IDs to delete
    var idsToDelete = existingDetails.Except(orderDetailDtos.Select(d => d.Id)).ToArray();
    await this.Delete(idsToDelete);

    return await this.Update(orderId, orderDetailDtos, existingDetails);
  }

  private async Task<IEnumerable<SellOrderDetail>> Update(int orderId, IEnumerable<UpdateSellOrderDetailDto> orderDetailDtos, IEnumerable<int> existingDetails)
  {
    // Get array of objects from orderDetails to update
    var orderDetailsToUpdate = orderDetailDtos.Where(d => existingDetails.Contains(d.Id)).ToArray();
    SetOrderId(orderId, orderDetailsToUpdate);
    // Get array of new details to create
    var newDetails = orderDetailDtos.Where(d => !existingDetails.Contains(d.Id)).ToArray();
    var orderDetails = (await this.Create(orderId, newDetails)).ToList();
    orderDetails.AddRange(await repository.Update(orderDetailsToUpdate));
    return orderDetails;
  }

  public async Task<IEnumerable<SellOrderDetail>> UpsertOrderDetailsByQuantityAsync(int sellOrderId, BaseBuyAllocationDto value, int newQuantity)
  {
    var sellPrice = value.SellPrice ?? throw new InvalidOperationException("Sell price cannot be null");
    var idCurrencySell = value.IdCurrencySell ?? throw new InvalidOperationException("Currency ID cannot be null");
    var sellOrderDetail = await GetByOrderId(sellOrderId, value.ProductId, sellPrice, idCurrencySell);
    if (sellOrderDetail is null)
    {
      // Create a new sell order detail with the specified product, sell price, and currency
      var newSellOrderDetail = NewSellOrderDetail(value, newQuantity);
      return await this.Create(sellOrderId, [newSellOrderDetail,]);
    }

    // Update the existing sell order detail with the new quantity
    sellOrderDetail.Quantity += newQuantity;
    var updateDto = mapper.Map<UpdateSellOrderDetailDto>(sellOrderDetail);
    return await this.Update(sellOrderId, [updateDto,]);
  }

  public CreateSellOrderDetailDto NewSellOrderDetail(BaseBuyAllocationDto value, int newQuantity) =>
    new CreateSellOrderDetailDto
    {
      IdProduct = value.ProductId,
      SellPrice = value.SellPrice ?? throw new InvalidOperationException("Sell price cannot be null"),
      IdCurrencySell = value.IdCurrencySell ?? throw new InvalidOperationException("Currency ID cannot be null"),
      Quantity = newQuantity,
    };

  public async Task Delete(IEnumerable<int> idsToDelete)
  {
    if (!idsToDelete.Any())
      return;
    await repository.DeleteById(idsToDelete);
  }

  private Task<IEnumerable<SellOrderDetail>> Create(int orderId, IEnumerable<UpdateSellOrderDetailDto> orderDetailDtos)
  {
    if (orderDetailDtos == null || !orderDetailDtos.Any())
    {
      return Task.FromResult<IEnumerable<SellOrderDetail>>([]);
    }
    SetOrderId(orderId, orderDetailDtos);
    return repository.Create(orderDetailDtos);
  }

  private static void SetOrderId(int orderId, IEnumerable<BaseSellOrderDetailDto> orderDetailDtos)
  {
    foreach (var detail in orderDetailDtos)
    {
      detail.SellOrderId = orderId;
    }
  }

  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
