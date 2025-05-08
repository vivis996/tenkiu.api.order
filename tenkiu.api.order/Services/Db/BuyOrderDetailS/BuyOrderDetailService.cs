using tenkiu.api.order.Models.Dto.BuyOrderDetail;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Repositories.BuyOrderDetailRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.BuyOrderDetailS;

public class BuyOrderDetailService(
  IBuyOrderDetailRepository repository
) : DisposableBase, IBuyOrderDetailService
{
  public async Task<IEnumerable<BuyOrderDetail>> GetByOrderId(int orderId)
  {
    return await repository.GetList(o => o.BuyOrderId == orderId);
  }

  public async Task<IEnumerable<int>> GetIdsByOrderId(int orderId)
  {
    return await repository.GetList(o => o.BuyOrderId == orderId, o => o.Id, null);
  }

  public Task<IEnumerable<BuyOrderDetail>> Create(int orderId, IEnumerable<CreateBuyOrderDetailDto> orderDetailDtos)
  {
    if (orderDetailDtos == null || !orderDetailDtos.Any())
    {
      throw new ArgumentException("Order detail DTOs cannot be null or empty.", nameof(orderDetailDtos));
    }
    SetOrderId(orderId, orderDetailDtos);
    return repository.Create(orderDetailDtos);
  }

  public async Task<IEnumerable<BuyOrderDetail>> Update(int orderId, IEnumerable<UpdateBuyOrderDetailDto> orderDetailDtos)
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
  
  private Task<IEnumerable<BuyOrderDetail>> Create(int orderId, IEnumerable<UpdateBuyOrderDetailDto> orderDetailDtos)
  {
    if (orderDetailDtos == null || !orderDetailDtos.Any())
    {
      return Task.FromResult<IEnumerable<BuyOrderDetail>>([]);
    }
    SetOrderId(orderId, orderDetailDtos);
    return repository.Create(orderDetailDtos);
  }

  private static void SetOrderId(int orderId, IEnumerable<BaseBuyOrderDetailDto> orderDetailDtos)
  {
    foreach (var detail in orderDetailDtos)
    {
      detail.BuyOrderId = orderId;
    }
  }

  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
