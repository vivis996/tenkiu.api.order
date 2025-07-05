using tenkiu.api.order.Models.Dto.DeliveryPeriod;
using tenkiu.api.order.Models.Entities;

namespace tenkiu.api.order.Services.Db.DeliveryPeriodS;

public interface IDeliveryPeriodService : IDisposable
{
  Task<DeliveryPeriod?> GetById(int id);
  Task<IEnumerable<DeliveryPeriod>> GetByIds(IEnumerable<int> ids);
  Task<IEnumerable<DeliveryPeriod>> GetAll();
  Task<IEnumerable<DeliveryPeriod>> GetOnlyActive();
  Task<DeliveryPeriod> Create(DeliveryPeriod deliveryPeriod);
  Task<DeliveryPeriod> Create(CreateDeliveryPeriodDto deliveryPeriod);
  Task<DeliveryPeriod> Update(DeliveryPeriod deliveryPeriod);
  Task<DeliveryPeriod> Update(UpdateDeliveryPeriodDto deliveryPeriod);
  Task<bool> Delete(int id);
}
