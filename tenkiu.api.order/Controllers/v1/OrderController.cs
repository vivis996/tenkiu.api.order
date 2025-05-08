using Microsoft.AspNetCore.Mvc;
using tenkiu.api.order.App.OrderApp;
using tenkiu.api.order.Controllers.Handler;
using tenkiu.api.order.Models.Dto.Order;
using tenkiu.api.order.Models.Request;
using vm.common.api.Models;

namespace tenkiu.api.order.Controllers.v1;

[ApiController]
[Route("api/order/v{version:apiVersion}/[controller]")]
[Asp.Versioning.ApiVersion("1.0")]
public class OrderController(
  IOrderAppService service
) : ApiBaseController
{
  /// <summary>
  /// Retrieves a specific order by their unique identifier.
  /// </summary>
  /// <param name="id">The ID of the order to retrieve.</param>
  /// <returns>A response containing the order data if found.</returns>
  [HttpGet("{id:int}")]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<ResponseOrderDto?>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<ResponseOrderDto?>> Get(int id)
  {
    return await service.GetById(id);
  }

  /// <summary>
  /// Retrieves a specific order by their unique hash.
  /// </summary>
  /// <param name="hash">The hash of the order to retrieve.</param>
  /// <returns>A response containing the order data if found.</returns>
  [HttpGet("{hash}")]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<ResponseOrderDto?>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<ResponseOrderDto?>> Get(string hash)
  {
    return await service.GetByHash(hash);
  }
  
  /// <summary>
  /// Searches orders with pagination based on query parameters. Accessible by admins only.
  /// </summary>
  /// <param name="searchRequest">The search and pagination criteria.</param>
  /// <returns>A response containing the paginated list of orders.</returns>
  [HttpGet]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<PaginationResponse<ResponseOrderDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<PaginationResponse<ResponseOrderDto>>> Get([FromQuery] OrderSearchRequest searchRequest)
  {
    return await service.GetByRequestPagination(searchRequest);
  }
  
  /// <summary>
  /// Creates a new order in the system.
  /// </summary>
  /// <param name="value">The order data to create.</param>
  /// <returns>A response containing the created order data.</returns>
  [HttpPost]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<int>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<int>> Post([FromBody] CreateOrderDto value)
  {
    return await service.Create(value);
  }
  
  /// <summary>
  /// Updates an existing order's data.
  /// </summary>
  /// <param name="value">The updated order data.</param>
  /// <returns>A response containing the updated order data.</returns>
  [HttpPut]
  [AuthorizeJwt]
  [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<bool>> Put([FromBody] UpdateOrderDto value)
  {
    return await service.Update(value);
  }
  
  /// <summary>
  /// Disposes of resources managed by this controller.
  /// </summary>
  protected override void DisposeResources()
  {
    service.Dispose();
  }
}
