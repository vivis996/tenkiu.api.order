using Microsoft.AspNetCore.Mvc;
using tenkiu.api.order.App.DeliveryPeriodApp;
using tenkiu.api.order.Controllers.Handler;
using tenkiu.api.order.Models.Common;
using tenkiu.api.order.Models.Dto.DeliveryPeriod;
using vm.common.api.Models;

namespace tenkiu.api.order.Controllers.v1;

[ApiController]
[Route("api/order/v{version:apiVersion}/[controller]")]
[Asp.Versioning.ApiVersion("1.0")]
public class DeliveryPeriodController(
  IDeliveryPeriodAppService service
) : ApiBaseController
{
  /// <summary>
  /// Retrieves a specific delivery period by its unique identifier.
  /// </summary>
  /// <param name="id">The ID of the delivery period to retrieve.</param>
  [HttpGet("{id:int}")]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<ResponseDeliveryPeriodDto?>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<ResponseDeliveryPeriodDto?>> Get(int id)
  {
    return await service.GetById(id);
  }

  /// <summary>
  /// Retrieves all delivery periods.
  /// </summary>
  [HttpGet]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ResponseDeliveryPeriodDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<IEnumerable<ResponseDeliveryPeriodDto>>> GetAll()
  {
    return await service.GetAll();
  }

  /// <summary>
  /// Retrieves only active delivery periods.
  /// </summary>
  [HttpGet("active")]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ResponseDeliveryPeriodDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<IEnumerable<ResponseDeliveryPeriodDto>>> GetOnlyActive()
  {
    return await service.GetOnlyActive();
  }

  /// <summary>
  /// Creates a new delivery period.
  /// </summary>
  [HttpPost]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<ResponseDeliveryPeriodDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<ResponseDeliveryPeriodDto>> Post([FromBody] CreateDeliveryPeriodDto value)
  {
    return await service.Create(value);
  }

  /// <summary>
  /// Updates an existing delivery period.
  /// </summary>
  [HttpPut]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<ResponseDeliveryPeriodDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<ResponseDeliveryPeriodDto>> Put([FromBody] UpdateDeliveryPeriodDto value)
  {
    return await service.Update(value);
  }

  /// <summary>
  /// Deletes a delivery period.
  /// </summary>
  [HttpDelete("{id:int}")]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<bool>> Delete(int id)
  {
    return await service.Delete(id);
  }
  /// <summary>
  /// Disposes of resources managed by this controller.
  /// </summary>
  protected override void DisposeResources()
  {
    service.Dispose();
  }
}
