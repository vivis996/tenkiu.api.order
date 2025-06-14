using tenkiu.api.order.Models.Dto.DeliveryPeriod;
using vm.common.api.Models;

namespace tenkiu.api.order.App.DeliveryPeriodApp;

public interface IDeliveryPeriodAppService : IDisposable
{
  Task<BaseResponse<ResponseDeliveryPeriodDto?>> GetById(int id);
  Task<BaseResponse<IEnumerable<ResponseDeliveryPeriodDto>>> GetAll();
  Task<BaseResponse<IEnumerable<ResponseDeliveryPeriodDto>>> GetOnlyActive();
  Task<BaseResponse<ResponseDeliveryPeriodDto>> Create(CreateDeliveryPeriodDto deliveryPeriod);
  Task<BaseResponse<ResponseDeliveryPeriodDto>> Update(UpdateDeliveryPeriodDto deliveryPeriod);
  Task<BaseResponse<bool>> Delete(int id);
}
