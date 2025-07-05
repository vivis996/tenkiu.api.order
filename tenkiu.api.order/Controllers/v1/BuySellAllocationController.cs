using Microsoft.AspNetCore.Mvc;
using tenkiu.api.order.App.BuySellAllocationApp;
using tenkiu.api.order.Controllers.Handler;
using tenkiu.api.order.Models.Common;
using tenkiu.api.order.Models.Dto.BuySellAllocation;
using vm.common.api.Models;

namespace tenkiu.api.order.Controllers.v1;

[ApiController]
[Route("api/order/v{version:apiVersion}/[controller]")]
[Asp.Versioning.ApiVersion("1.0")]
public class BuySellAllocationController(
  IBuySellAllocationAppService service
) : ApiBaseController
{
  /// <summary>
  /// Retrieves a specific Buy-Sell allocation by its unique identifier.
  /// </summary>
  /// <param name="id">The ID of the allocation to retrieve.</param>
  /// <returns>A response containing the allocation data if found.</returns>
  [HttpGet("{id:int}")]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<ResponseBuySellAllocationDto?>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<ResponseBuySellAllocationDto?>> Get(int id)
  {
    return await service.GetById(id);
  }

  /// <summary>
  /// Retrieves all allocations associated with a specific sell order.
  /// </summary>
  /// <param name="id">The ID of the sell order.</param>
  /// <returns>A list of matching allocations.</returns>
  [HttpGet("sell/{id:int}")]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ResponseBuySellAllocationDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetBySellOrderId(int id)
  {
    return await service.GetBySellOrderId(id);
  }

  /// <summary>
  /// Retrieves all allocations associated with a specific buy order.
  /// </summary>
  /// <param name="id">The ID of the buy order.</param>
  /// <returns>A list of matching allocations.</returns>
  [HttpGet("buy/{id:int}")]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ResponseBuySellAllocationDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetByBuyOrderId(int id)
  {
    return await service.GetByBuyOrderId(id);
  }

  /// <summary>
  /// Retrieves all allocations associated with a specific sell order detail.
  /// </summary>
  /// <param name="id">The ID of the sell order detail.</param>
  /// <returns>A list of matching allocations.</returns>
  [HttpGet("sell/detail/{id:int}")]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ResponseBuySellAllocationDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetBySellOrderDetailId(int id)
  {
    return await service.GetBySellOrderDetailId(id);
  }

  /// <summary>
  /// Retrieves all allocations associated with a specific buy order detail.
  /// </summary>
  /// <param name="id">The ID of the buy order detail.</param>
  /// <returns>A list of matching allocations.</returns>
  [HttpGet("buy/detail/{id:int}")]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ResponseBuyAllocationDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<IEnumerable<ResponseBuyAllocationDto>>> GetByBuyOrderDetailId(int id)
  {
    return await service.GetByBuyOrderDetailId(id);
  }



  /// <summary>
  /// Creates a new Buy-Sell allocation.
  /// </summary>
  /// <param name="value">The allocation data to create.</param>
  /// <returns>A response containing the ID of the newly created allocation.</returns>
  [HttpPost]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<int>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<int>> Post([FromBody] CreateBuySellAllocationDto value)
  {
    return await service.Create(value);
  }

  /// <summary>
  /// Updates an existing Buy-Sell allocation.
  /// </summary>
  /// <param name="value">The updated allocation data.</param>
  /// <returns>A response indicating whether the update was successful.</returns>
  [HttpPut]
  [AuthorizeJwt(UserType.Admin)]
  [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status500InternalServerError)]
  public async Task<BaseResponse<bool>> Put([FromBody] UpdateBuySellAllocationDto value)
  {
    return await service.Update(value);
  }

  /// <summary>
  /// Deletes a specific Buy-Sell allocation by ID.
  /// </summary>
  /// <param name="id">The ID of the allocation to delete.</param>
  /// <returns>A response indicating whether the deletion was successful.</returns>
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
