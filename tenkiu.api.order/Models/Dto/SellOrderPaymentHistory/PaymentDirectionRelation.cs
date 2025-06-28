namespace tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;

public class PaymentDirectionRelation
{
  public PaymentDirection Direction { get; set; }
  public string Description { get; set; }
  public IEnumerable<PaymentReasonDescription> Reasons { get; set; }
}
