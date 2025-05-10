namespace tenkiu.api.order.Models.Dto.OrderDetail;

public abstract class BaseOrderDetailDto
{
  public int IdOrder { get; set; }
  public int IdProduct { get; set; }
  public decimal ListedPrice { get; set; }
  public int IdCurrencyListed { get; set; }
  public decimal PurchasePrice { get; set; }
  public decimal PurchasePriceTax { get; set; }
  public int IdCurrencyPurchase { get; set; }
  public decimal SellPrice { get; set; }
  public int IdCurrencySell { get; set; }
  public int Quantity { get; set; }
}
