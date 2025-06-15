using tenkiu.api.order.Models.Dto.DeliveryPeriod;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Repositories.DeliveryPeriodRepo;
using vm.common;

namespace tenkiu.api.order.Services.Db.DeliveryPeriodS;

public class DeliveryPeriodService(
  IDeliveryPeriodRepository repository
) : DisposableBase, IDeliveryPeriodService
{
  public Task<DeliveryPeriod?> GetById(int id)
  {
    return repository.GetById(id);
  }

  public async Task<IEnumerable<DeliveryPeriod>> GetAll()
  {
    return await repository.GetAll();
  }

  public async Task<IEnumerable<DeliveryPeriod>> GetOnlyActive()
  {
    return await repository.GetList(d => d.IsActive);
  }

  public Task<DeliveryPeriod> Create(DeliveryPeriod deliveryPeriod)
  {
    return repository.Create(deliveryPeriod);
  }

  public Task<DeliveryPeriod> Create(CreateDeliveryPeriodDto deliveryPeriod)
  {
    return repository.Create(deliveryPeriod);
  }

  public Task<DeliveryPeriod> Update(DeliveryPeriod deliveryPeriod)
  {
    return repository.Update(deliveryPeriod);
  }

  public Task<DeliveryPeriod> Update(UpdateDeliveryPeriodDto deliveryPeriod)
  {
    return repository.Update(deliveryPeriod);
  }

  public async Task<bool> Delete(int id)
  {
    var deliveryPeriod = await repository.GetById(id);
    if (deliveryPeriod is null)
    {
      return false;
    }
    deliveryPeriod.IsActive = false;
    await repository.Update(deliveryPeriod);

    return true;
  }

  protected override void DisposeResources()
  {
    repository.Dispose();
  }
}
