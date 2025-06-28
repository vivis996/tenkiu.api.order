namespace tenkiu.api.order.Models.Dto.BuyOrderDetail;

public abstract class BaseBuyOrderDetailDto
{
  /// <summary>
  /// FK to the product being purchased
  /// </summary>
  public int IdProduct { get; set; }

  /// <summary>
  /// Foreign key referencing the parent purchase order
  /// </summary>
  public int BuyOrderId { get; set; }

  /// <summary>
  /// Price per unit of product
  /// </summary>
  public decimal PurchasePrice { get; set; }

  /// <summary>
  /// Tax applied to the purchase price
  /// </summary>
  public decimal PurchasePriceTax { get; set; }

  /// <summary>
  /// Currency code for the purchase price
  /// </summary>
  public int IdCurrencyPurchase { get; set; }

  /// <summary>
  /// Number of units bought.
  /// </summary>
  public int Quantity { get; set; }

  /// <summary>
  /// Total converted purchase price in base currency
  /// </summary>
  public decimal ConvertedPurchasePrice { get; set; }
}
