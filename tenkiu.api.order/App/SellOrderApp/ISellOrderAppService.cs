using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Request;
using vm.common.api.Models;

namespace tenkiu.api.order.App.SellOrderApp;

public interface ISellOrderAppService : IDisposable
{
  /// <summary>
  /// Retrieves an order by its unique identifier.
  /// </summary>
  /// <param name="id">The ID of the order to retrieve.</param>
  /// <returns>A response containing the order data if found, or a failure message if not.</returns>
  Task<BaseResponse<ResponseSellOrderDto?>> GetById(int id);
  
  /// <summary>
  /// Retrieves an order by its unique hash.
  /// </summary>
  /// <param name="hash">The hash of the order to retrieve.</param>
  /// <returns>A response containing the order data if found, or a failure message if not.</returns>
  Task<BaseResponse<ResponseSellOrderDto?>> GetByHash(string hash);
  
  /// <summary>
  /// Searches for orders with pagination based on the provided criteria.
  /// </summary>
  /// <param name="searchRequest">The search and pagination parameters.</param>
  /// <returns>A response containing a paginated list of orders matching the criteria.</returns>
  Task<BaseResponse<PaginationResponse<ResponseSellOrderDto>>> GetByRequestPagination(SellOrderSearchRequest searchRequest);
  
  /// <summary>
  /// Creates a new order in the system.
  /// </summary>
  /// <param name="value">The order data to create.</param>
  /// <returns>A response containing the ID of the created order, or a failure message if creation fails.</returns>
  Task<BaseResponse<int>> Create(CreateSellOrderDto value);
  
  /// <summary>
  /// Updates an existing order's data.
  /// </summary>
  /// <param name="value">The updated order data.</param>
  /// <returns>A response indicating whether the update was successful.</returns>
  Task<BaseResponse<bool>> Update(UpdateSellOrderDto value);
}
