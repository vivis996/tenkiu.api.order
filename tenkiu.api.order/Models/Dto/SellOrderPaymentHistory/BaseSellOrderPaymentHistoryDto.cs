namespace tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;

public abstract class BaseSellOrderPaymentHistoryDto
{
  public decimal Amount { get; set; }
  public int IdCurrency { get; set; }
  public PaymentDirection PaymentDirection { get; set; }
  public PaymentReason PaymentReason { get; set; }
  public PaymentType PaymentType { get; set; }
  public string? Notes { get; set; }
  public DateOnly PaymentDate { get; set; }
}
