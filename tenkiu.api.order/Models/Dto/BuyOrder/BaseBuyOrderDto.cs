namespace tenkiu.api.order.Models.Dto.BuyOrder;

public abstract class BaseBuyOrderDto
{
  /// <summary>
  /// Date of the purchase order
  /// </summary>
  public DateOnly PurchaseDate { get; set; }

  public int IdStore { get; set; }

  /// <summary>
  /// Base currency code for this purchase
  /// </summary>
  public int BaseCurrencyId { get; set; }

  /// <summary>
  /// Currency to which the purchase price is converted
  /// </summary>
  public int ConvertedCurrencyId { get; set; }
  /// <summary>
  /// Exchange rate used to convert purchase currency to base currency
  /// </summary>
  public decimal ExchangeRate { get; set; }
}
