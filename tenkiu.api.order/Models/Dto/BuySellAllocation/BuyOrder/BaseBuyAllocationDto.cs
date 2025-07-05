namespace tenkiu.api.order.Models.Dto.BuySellAllocation.BuyOrder;

public abstract class BaseBuyAllocationDto
{
  public int BuyOrderDetailId { get; set; }
  public int IdClient { get; set; }
  public int DeliveryPeriodId { get; set; }
  public int ProductId { get; set; }
  public int Quantity { get; set; }
  public decimal? SellPrice { get; set; }
  public int? IdCurrencySell { get; set; }
}
