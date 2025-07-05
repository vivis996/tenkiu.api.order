namespace tenkiu.api.order.Models.Dto.BuySellAllocation;

public class BaseBuySellAllocationDto
{
  public int BuyOrderDetailId { get; set; }
  public int SellOrderDetailId { get; set; }
  public decimal Quantity { get; set; }
}
