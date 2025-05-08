namespace tenkiu.api.order.Models.Dto.SellOrder;

public abstract class BaseSellOrderDto
{
  public int IdClient { get; set; }
  public string DeliverySeason { get; set; }
  public DateOnly DeliveryDate { get; set; }
}
