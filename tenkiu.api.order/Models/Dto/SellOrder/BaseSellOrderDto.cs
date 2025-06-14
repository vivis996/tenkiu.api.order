namespace tenkiu.api.order.Models.Dto.SellOrder;

public abstract class BaseSellOrderDto
{
  public int IdClient { get; set; }
  public int DeliveryPeriodId { get; set; }
  public DateOnly DeliveryDate { get; set; }
  public int BaseCurrencyId { get; set; }
}
