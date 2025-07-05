using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.BuySellAllocation;

public class ResponseBuySellAllocationDto : BaseBuySellAllocationDto, IIdModel<int>
{
  /// <summary>
  /// Primary key of the allocation
  /// </summary>
  public int Id { get; set; }
}

public class ClientSellAllocationSummaryDto
{
  public int IdClient { get; set; }
  public int TotalQuantity { get; set; }
  public IEnumerable<SellOrderAllocationDto> Orders { get; set; }
}

public class ClientPeriodProductAllocationDto
{
  public int IdClient { get; set; }
  public int SellOrderId { get; set; }
  public int DeliveryPeriodId { get; set; }
  public string Hash { get; set; }
  public decimal TotalQuantity { get; set; }
  public decimal TotalAssign { get; set; }
  public decimal PendingAssign => this.TotalQuantity - this.TotalAssign;
  public int ProductId { get; set; }
}

public class SellOrderAllocationDto
{
  public int SellOrderId { get; set; }
  public int DeliveryPeriodId { get; set; }
  public string Hash { get; set; }
  public IEnumerable<SellOrderDetailAllocationDto> Details { get; set; }
}

public class SellOrderDetailAllocationDto : BaseBuySellAllocationDto, IIdModel<int>
{
  /// <summary>
  /// Primary key of the allocation
  /// </summary>
  public int Id { get; set; }
  public decimal SellPrice { get; set; }
  public int IdCurrencySell { get; set; }
}
