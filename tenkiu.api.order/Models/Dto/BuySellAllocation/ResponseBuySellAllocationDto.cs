using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.BuySellAllocation;

public class ResponseBuySellAllocationDto : BaseBuySellAllocationDto, IIdModel<int>
{
  /// <summary>
  /// Primary key of the allocation
  /// </summary>
  public int Id { get; set; }
}


public class ResponseBuyAllocationDto
{
  public int IdClient { get; set; }
  public decimal TotalQuantity { get; set; }
  public IEnumerable<ClientSellOrderAllocationDto> Orders { get; set; }
}

public class ClientSellOrderAllocationDto
{
  public int SellOrderId { get; set; }
  public int DeliveryPeriodId { get; set; }
  public string Hash { get; set; }
  public IEnumerable<ClientSellOrderDetailAllocationDto> Details { get; set; }
}

public class ClientSellOrderDetailAllocationDto : BaseBuySellAllocationDto, IIdModel<int>
{
  /// <summary>
  /// Primary key of the allocation
  /// </summary>
  public int Id { get; set; }
  public decimal SellPrice { get; set; }
  public int IdCurrencySell { get; set; }
}
