using AutoMapper;
using tenkiu.api.order.Models.Dto.BuySellAllocation;
using tenkiu.api.order.Services.Db.BuySellAllocationS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.App.BuySellAllocationApp;

public class BuySellAllocationAppService(
  IBuySellAllocationService service,
  IMapper mapper
) : DisposableBase, IBuySellAllocationAppService
{
  public async Task<BaseResponse<ResponseBuySellAllocationDto?>> GetById(int id)
  {
    var allocation = await service.GetById(id);
    if (allocation is null)
      return new FailureResponse<ResponseBuySellAllocationDto?>("Allocation not found");

    return new SuccessResponse<ResponseBuySellAllocationDto?>(mapper.Map<ResponseBuySellAllocationDto>(allocation));
  }

  public async Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetBySellOrderId(int id)
  {
    var values = await service.GetBySellOrderId(id);
    var allocations = mapper.Map<IEnumerable<ResponseBuySellAllocationDto>>(values);
    return new SuccessResponse<IEnumerable<ResponseBuySellAllocationDto>>(allocations);
  }

  public async Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetByBuyOrderId(int id)
  {
    var values = await service.GetByBuyOrderId(id);
    var allocations = mapper.Map<IEnumerable<ResponseBuySellAllocationDto>>(values);
    return new SuccessResponse<IEnumerable<ResponseBuySellAllocationDto>>(allocations);
  }

  public async Task<BaseResponse<IEnumerable<ResponseBuySellAllocationDto>>> GetBySellOrderDetailId(int id)
  {
    var values = await service.GetBySellOrderDetailId(id);
    var allocations = mapper.Map<IEnumerable<ResponseBuySellAllocationDto>>(values);
    return new SuccessResponse<IEnumerable<ResponseBuySellAllocationDto>>(allocations);
  }

  public async Task<BaseResponse<IEnumerable<ResponseBuyAllocationDto>>> GetByBuyOrderDetailId(int id)
  {
    var values = await service.GetByBuyOrderDetailId(id);
    return new SuccessResponse<IEnumerable<ResponseBuyAllocationDto>>(values);
  }

  public async Task<BaseResponse<int>> Create(CreateBuySellAllocationDto value)
  {
    var result = await service.Create(value);
    return new SuccessResponse<int>(result.Id);
  }

  public async Task<BaseResponse<bool>> Update(UpdateBuySellAllocationDto value)
  {
    var result = await service.Update(value);
    return new SuccessResponse<bool>(result is not null);
  }

  public async Task<BaseResponse<bool>> Delete(int id)
  {
    var result = await service.Delete(id);
    return new SuccessResponse<bool>(result);
  }

  protected override void DisposeResources()
  {
    service.Dispose();
  }
}
