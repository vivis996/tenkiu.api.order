using AutoMapper;
using tenkiu.api.order.Models.Dto.DeliveryPeriod;
using tenkiu.api.order.Services.Db.DeliveryPeriodS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.App.DeliveryPeriodApp;

public class DeliveryPeriodAppService(
  IDeliveryPeriodService service,
  IMapper mapper
) : DisposableBase, IDeliveryPeriodAppService
{
  public async Task<BaseResponse<ResponseDeliveryPeriodDto?>> GetById(int id)
  {
    var deliveryPeriod = await service.GetById(id);
    if (deliveryPeriod is null)
      return new FailureResponse<ResponseDeliveryPeriodDto?>("Delivery period not found");
    return new SuccessResponse<ResponseDeliveryPeriodDto?>(mapper.Map<ResponseDeliveryPeriodDto>(deliveryPeriod));
  }

  public async Task<BaseResponse<IEnumerable<ResponseDeliveryPeriodDto>>> GetAll()
  {
    var values = (await service.GetAll()).ToArray();
    if (values.Length == 0)
      return new SuccessResponse<IEnumerable<ResponseDeliveryPeriodDto>>([]).AddMessage("No delivery periods found");
    var mappedValues = mapper.Map<IEnumerable<ResponseDeliveryPeriodDto>>(values);
    return new SuccessResponse<IEnumerable<ResponseDeliveryPeriodDto>>(mappedValues);
  }

  public async Task<BaseResponse<IEnumerable<ResponseDeliveryPeriodDto>>> GetOnlyActive()
  {
    var values = (await service.GetOnlyActive()).ToArray();
    if (values.Length == 0)
      return new SuccessResponse<IEnumerable<ResponseDeliveryPeriodDto>>([]).AddMessage("No delivery periods found");
    var mappedValues = mapper.Map<IEnumerable<ResponseDeliveryPeriodDto>>(values);
    return new SuccessResponse<IEnumerable<ResponseDeliveryPeriodDto>>(mappedValues);
  }

  public async Task<BaseResponse<ResponseDeliveryPeriodDto>> Create(CreateDeliveryPeriodDto deliveryPeriod)
  {
    var @object = await service.Create(deliveryPeriod);
    if (@object is null)
      return new FailureResponse<ResponseDeliveryPeriodDto>("Failed to create delivery period");
    return new SuccessResponse<ResponseDeliveryPeriodDto>(mapper.Map<ResponseDeliveryPeriodDto>(@object));
  }

  public async Task<BaseResponse<ResponseDeliveryPeriodDto>> Update(UpdateDeliveryPeriodDto deliveryPeriod)
  {
    var @object = await service.Update(deliveryPeriod);
    if (@object is null)
      return new FailureResponse<ResponseDeliveryPeriodDto>("Failed to update delivery period");
    return new SuccessResponse<ResponseDeliveryPeriodDto>(mapper.Map<ResponseDeliveryPeriodDto>(@object));
  }

  public async Task<BaseResponse<bool>> Delete(int id)
  {
    var result = await service.Delete(id);
    if (!result)
      return new FailureResponse<bool>("Failed to delete delivery period or not found");
    return new SuccessResponse<bool>(true);
  }

  /// <summary>
  /// Disposes of resources managed by this service.
  /// </summary>
  protected override void DisposeResources()
  {
    // Dispose of the service to release unmanaged resources
    service.Dispose();
  }
}
