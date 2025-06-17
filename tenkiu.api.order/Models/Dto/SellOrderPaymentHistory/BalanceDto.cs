namespace tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;

public class BalanceDto
{
  public int IdCurrency { get; set; }
  public decimal Inflow { get; set; }
  public decimal Outflow { get; set; }
  public decimal Balance => this.Inflow - this.Outflow;
}
