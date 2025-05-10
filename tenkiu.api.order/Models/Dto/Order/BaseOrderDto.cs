namespace tenkiu.api.order.Models.Dto.Order;

public abstract class BaseOrderDto
{
  public int IdClient { get; set; }
  public string DeliverySeason { get; set; }
  public DateOnly DeliveryDate { get; set; }
}
