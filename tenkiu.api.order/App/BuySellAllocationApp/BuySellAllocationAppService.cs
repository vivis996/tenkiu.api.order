using AutoMapper;
using tenkiu.api.order.Models.Dto.BuySellAllocation;
using tenkiu.api.order.Models.Dto.BuySellAllocation.BuyOrder;
using tenkiu.api.order.Services.Db.BuySellAllocationS;
using tenkiu.api.order.Services.Db.SellOrderDetailS;
using tenkiu.api.order.Services.Db.SellOrderS;
using vm.common;
using vm.common.api.Models;

namespace tenkiu.api.order.App.BuySellAllocationApp;

public class BuySellAllocationAppService(
  IBuySellAllocationService service,
  ISellOrderService sellOrderService,
  ISellOrderDetailService sellOrderDetailService,
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

  public async Task<BaseResponse<IEnumerable<ClientSellAllocationSummaryDto>>> GetByBuyOrderDetailId(int id)
  {
    var values = await service.GetByBuyOrderDetailId(id);
    return new SuccessResponse<IEnumerable<ClientSellAllocationSummaryDto>>(values);
  }

  public async Task<BaseResponse<ClientPeriodProductAllocationDto>> GetByClientPeriodAndProductId(
    int idClient, int idPeriod, int idProduct)
  {
    var sellOrderId = await sellOrderService.GetIdByClientAndPeriod(idClient, idPeriod);
    if (sellOrderId is null)
      return new SuccessResponse<ClientPeriodProductAllocationDto>().AddMessage("There's no order in this period for this client");

    var sellOrder = await sellOrderService.GetProductOrderById(sellOrderId.Value, idProduct);
    if (sellOrder is null)
      return new SuccessResponse<ClientPeriodProductAllocationDto>().AddMessage("Product was not found in this period for this client");
    var ids = sellOrder.SellOrderDetails.Select(d => d.Id).ToArray();
    var clientSummary = (await service.GetBuyOrderDetailBySellOrderDetilId(ids)).FirstOrDefault();
    var result = new ClientPeriodProductAllocationDto
    {
      IdClient = idClient,
      SellOrderId = sellOrderId.Value,
      DeliveryPeriodId = idPeriod,
      ProductId = idProduct,
      Hash = sellOrder.Hash,
      TotalQuantity = sellOrder.SellOrderDetails.Sum(d => d.Quantity),
      TotalAssign =  clientSummary?.TotalQuantity ?? 0,
    };
    return new SuccessResponse<ClientPeriodProductAllocationDto>(result);
  }

  public async Task<BaseResponse<int>> Create(CreateBuyAllocationDto value)
  {
    var result = await service.CreateWithTransaction(value);
    if (!result.Success)
      return new FailureResponse<int>(result.Message);
    return new SuccessResponse<int>(result.SellOrderId);
  }

  public async Task<BaseResponse<bool>> Update(UpdateBuyAllocationDto value)
  {
    var result = await service.UpdateWithTransaction(value);
    if (!result.Success)
      return new FailureResponse<bool>(result.Message);

    return new SuccessResponse<bool>(true);
  }

  public async Task<BaseResponse<bool>> Delete(int id)
  {
    var result = await service.Delete(id);
    return new SuccessResponse<bool>(result);
  }

  protected override void DisposeResources()
  {
    service.Dispose();
    sellOrderService.Dispose();
    sellOrderDetailService.Dispose();
  }
}

public record AllocationChangesDto(
  List<CreateBuySellAllocationDto> AllocationToCreate,
  List<UpdateBuySellAllocationDto> AllocationToUpdate
);

public record AllocationMapEntry(int Id, int Quantity);
