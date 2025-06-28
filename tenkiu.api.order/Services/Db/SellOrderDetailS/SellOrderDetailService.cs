using tenkiu.api.order.Models.Dto.SellOrderDetail;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Repositories.SellOrderDetailRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.SellOrderDetailS;

public class SellOrderDetailService(
  ISellOrderDetailRepository repository
) : DisposableBase, ISellOrderDetailService
{
  public async Task<IEnumerable<SellOrderDetail>> GetByOrderId(int orderId)
  {
    return await repository.GetList(o => o.SellOrderId == orderId);
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
    // Get array of IDs to delete
    var idsToDelete = existingDetails.Except(orderDetailDtos.Select(d => d.Id)).ToArray();
    // Get array of objects from orderDetails to update
    var orderDetailsToUpdate = orderDetailDtos.Where(d => existingDetails.Contains(d.Id)).ToArray();
    SetOrderId(orderId, orderDetailsToUpdate);
    
    // Get array of new details to create
    var newDetails = orderDetailDtos.Where(d => !existingDetails.Contains(d.Id)).ToArray();
    var orderDetails = (await this.Create(orderId, newDetails)).ToList();
    orderDetails.AddRange(await repository.Update(orderDetailsToUpdate));
    if (idsToDelete.Length != 0)
    {
      await repository.DeleteById(idsToDelete);
    }
    
    return orderDetails;
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
