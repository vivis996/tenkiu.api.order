namespace tenkiu.api.order.Models.Dto.BuySellAllocation;

public abstract class BaseBuySellAllocationDto
{
  public int BuyOrderDetailId { get; set; }
  public int SellOrderDetailId { get; set; }
  public int Quantity { get; set; }
}
