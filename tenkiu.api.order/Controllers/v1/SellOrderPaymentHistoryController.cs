using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using tenkiu.api.order.App.SellOrderPaymentHistoryApp;
using tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;
using tenkiu.api.order.Models;
using vm.common.api.Models;
using System.Collections.Generic;
using tenkiu.api.order.Controllers.Handler;

namespace tenkiu.api.order.Controllers.v1;

[ApiController]
[Route("api/order/v{version:apiVersion}/[controller]")]
[Asp.Versioning.ApiVersion("1.0")]
public class SellOrderPaymentHistoryController(
  ISellOrderPaymentHistoryAppService service
) : ApiBaseController
{
  /// <summary>
  /// Disposes of resources managed by this controller.
  /// </summary>
  protected override void DisposeResources()
  {
    service.Dispose();
  }
  
  /// <summary>
  /// Retrieves a specific payment history record by its unique identifier.
  /// </summary>
  /// <param name="id">The ID of the payment history record to retrieve.</param>
  /// <returns>A response containing the payment history data if found.</returns>
  [HttpGet("{id:int}")]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<ResponseSellOrderPaymentHistoryDto?>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<ResponseSellOrderPaymentHistoryDto?>> Get(int id)
  {
    return await service.GetById(id);
  }

  /// <summary>
  /// Retrieves payment history records for a specific sell order.
  /// </summary>
  /// <param name="sellOrderId">The ID of the sell order.</param>
  /// <returns>A response containing the list of payment history records.</returns>
  [HttpGet("sellorder/{sellOrderId:int}")]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ResponseSellOrderPaymentHistoryDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<IEnumerable<ResponseSellOrderPaymentHistoryDto>>> GetBySellOrderId(int sellOrderId)
  {
    return await service.GetBySellOrderId(sellOrderId);
  }

  /// <summary>
  /// Creates a new payment history record for a sell order.
  /// </summary>
  /// <param name="value">The payment history data to create.</param>
  /// <returns>A response containing the created payment history record.</returns>
  [HttpPost]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<int>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<int>> Post([FromBody] CreateSellOrderPaymentHistoryDto value)
  {
    return await service.Create(value);
  }

  /// <summary>
  /// Updates an existing payment history record.
  /// </summary>
  /// <param name="value">The updated payment history data.</param>
  /// <returns>A response containing the updated payment history record.</returns>
  [HttpPut]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<bool>> Put([FromBody] UpdateSellOrderPaymentHistoryDto value)
  {
    return await service.Update(value);
  }

  /// <summary>
  /// Retrieves available payment directions and their reasons.
  /// </summary>
  /// <returns>A response containing a mapping of payment directions to their reasons.</returns>
  [HttpGet("paymentDirections/relation")]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<IEnumerable<PaymentDirectionRelation>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<IEnumerable<PaymentDirectionRelation>>> GetPaymentDirections()
  {
    return await service.GetDirectionRelations();
  }

  /// <summary>
  /// Retrieves descriptions of available payment types.
  /// </summary>
  /// <returns>A response containing payment type descriptions</returns>
  [HttpGet("paymentTypes/descriptions")]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<IEnumerable<PaymentTypeDescription>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<IEnumerable<PaymentTypeDescription>>> GetPaymentTypeDescriptions()
  {
    return await service.GetPaymentTypeDescriptions();
  }

}
