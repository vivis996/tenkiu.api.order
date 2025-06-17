using AutoMapper;
using tenkiu.api.order.Models;
using tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;
using tenkiu.api.order.Services.Db.SellOrderPaymentHistoryS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.App.SellOrderPaymentHistoryApp;

public class SellOrderPaymentHistoryAppService(
  ISellOrderPaymentHistoryService service,
  IMapper mapper
) : DisposableBase, ISellOrderPaymentHistoryAppService
{
  /// <summary>
  /// Retrieves a payment history record by its unique identifier.
  /// </summary>
  /// <param name="id">The ID of the payment history record to retrieve.</param>
  /// <returns>A response containing the payment history data if found, or a failure message if not.</returns>
  public async Task<BaseResponse<ResponseSellOrderPaymentHistoryDto?>> GetById(int id)
  {
    var value = await service.GetById(id);
    if (value is null)
      return new FailureResponse<ResponseSellOrderPaymentHistoryDto?>().AddMessage("Sell order payment history not found.");
    var paymentHistoryDto = mapper.Map<ResponseSellOrderPaymentHistoryDto>(value);
    return new SuccessResponse<ResponseSellOrderPaymentHistoryDto?>(paymentHistoryDto);
  }

  /// <summary>
  /// Retrieves all payment history records associated with a specific sell order.
  /// </summary>
  /// <param name="sellOrderId">The ID of the sell order.</param>
  /// <returns>A response containing the list of payment history records, or an empty list if none exist.</returns>
  public async Task<BaseResponse<IEnumerable<ResponseSellOrderPaymentHistoryDto>>> GetBySellOrderId(int sellOrderId)
  {
    var values = await service.GetBySellOrderId(sellOrderId);
    if (values is null || !values.Any())
      return new SuccessResponse<IEnumerable<ResponseSellOrderPaymentHistoryDto>>().AddMessage("No payment history found for the specified sell order.");

    values = values.OrderByDescending(x => x.PaymentDate).ToArray();
    var paymentHistoryDtos = mapper.Map<IEnumerable<ResponseSellOrderPaymentHistoryDto>>(values);
    return new SuccessResponse<IEnumerable<ResponseSellOrderPaymentHistoryDto>>(paymentHistoryDtos);
  }

  /// <summary>
  /// Creates a new payment history record for a sell order.
  /// </summary>
  /// <param name="value">The data for the new payment history record.</param>
  /// <returns>A response containing the created payment history record, or a failure message if creation fails.</returns>
  public async Task<BaseResponse<int>> Create(CreateSellOrderPaymentHistoryDto value)
  {
    var @object = await service.Create(value);
    if (@object is null)
      return new FailureResponse<int>().AddMessage("Failed to create sell order payment history.");

    return new SuccessResponse<int>(@object.Id);
  }

  /// <summary>
  /// Updates an existing payment history record.
  /// </summary>
  /// <param name="value">The updated payment history data.</param>
  /// <returns>A response containing the updated payment history record, or a failure message if update fails.</returns>
  public async Task<BaseResponse<bool>> Update(UpdateSellOrderPaymentHistoryDto value)
  {
    var @object = await service.Update(value);
    if (@object is null)
      return new FailureResponse<bool>().AddMessage("Failed to update sell order payment history.");

    return new SuccessResponse<bool>(@object is not null);
  }

  /// <summary>
  /// Retrieves all available payment directions along with their reasons.
  /// </summary>
  /// <returns>A response containing a dictionary mapping payment directions to their reasons, or a failure message if none are found.</returns>
  public async Task<BaseResponse<IEnumerable<PaymentDirectionRelation>>> GetDirectionRelations()
  {
    var paymentDirections = await service.GetDirectionRelations();
    if (paymentDirections is null || !paymentDirections.Any())
      return new SuccessResponse<IEnumerable<PaymentDirectionRelation>>().AddMessage("No payment directions found.");

    return new SuccessResponse<IEnumerable<PaymentDirectionRelation>>(paymentDirections);
  }

  /// <summary>
  /// Retrieves all available payment type descriptions.
  /// </summary>
  /// <returns>A response containing payment type descriptions</returns>
  public async Task<BaseResponse<IEnumerable<PaymentTypeDescription>>> GetPaymentTypeDescriptions()
  {
    var paymentTypeDescriptions = await service.GetPaymentTypeDescriptions();
    if (paymentTypeDescriptions is null || !paymentTypeDescriptions.Any())
      return new SuccessResponse<IEnumerable<PaymentTypeDescription>>().AddMessage("No payment type descriptions found.");

    return new SuccessResponse<IEnumerable<PaymentTypeDescription>>(paymentTypeDescriptions);
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
