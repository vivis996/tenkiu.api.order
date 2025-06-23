using tenkiu.api.order.Models;
using tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;
using vm.common.api.Models;

namespace tenkiu.api.order.App.SellOrderPaymentHistoryApp;

public interface ISellOrderPaymentHistoryAppService : IDisposable
{
  /// <summary>
  /// Retrieves a payment history record by its unique identifier.
  /// </summary>
  /// <param name="id">The ID of the payment history record to retrieve.</param>
  /// <returns>A response containing the payment history data if found, or a failure message if not.</returns>
  Task<BaseResponse<ResponseSellOrderPaymentHistoryDto?>> GetById(int id);

  /// <summary>
  /// Retrieves all payment history records associated with a specific sell order.
  /// </summary>
  /// <param name="sellOrderId">The ID of the sell order.</param>
  /// <returns>A response containing the list of payment history records, or an empty list if none exist.</returns>
  Task<BaseResponse<IEnumerable<ResponseSellOrderPaymentHistoryDto>>> GetBySellOrderId(int sellOrderId);

  /// <summary>
  /// Creates a new payment history record for a sell order.
  /// </summary>
  /// <param name="value">The data for the new payment history record.</param>
  /// <returns>A response containing the created payment history record, or a failure message if creation fails.</returns>
  Task<BaseResponse<int>> Create(CreateSellOrderPaymentHistoryDto value);

  /// <summary>
  /// Updates an existing payment history record.
  /// </summary>
  /// <param name="value">The updated payment history data.</param>
  /// <returns>A response containing the updated payment history record, or a failure message if update fails.</returns>
  Task<BaseResponse<bool>> Update(UpdateSellOrderPaymentHistoryDto value);

  /// <summary>
  /// Retrieves all available payment directions along with their reasons.
  /// </summary>
  /// <returns>A response containing a dictionary mapping payment directions to their reasons, or a failure message if none are found.</returns>
  Task<BaseResponse<IEnumerable<PaymentDirectionRelation>>> GetDirectionRelations();
  
  /// <summary>
  /// Retrieves descriptions for all available payment types.
  /// </summary>
  /// <returns>A response containing payment type descriptions</returns>
  Task<BaseResponse<IEnumerable<PaymentTypeDescription>>> GetPaymentTypeDescriptions();
}
