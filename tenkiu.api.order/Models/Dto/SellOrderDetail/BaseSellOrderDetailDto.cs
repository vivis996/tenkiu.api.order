namespace tenkiu.api.order.Models.Dto.SellOrderDetail;

public abstract class BaseSellOrderDetailDto
{
  public int SellOrderId { get; set; }
  public int SellOrderDetailId { get; set; }
  public int IdProduct { get; set; }
  public decimal SellPrice { get; set; }
  public int IdCurrencySell { get; set; }
  public int BaseCurrencyId { get; set; }
  public decimal SellExchangeRate { get; set; }
  public decimal ConvertedSellPrice { get; set; }
  public decimal ProfitBase { get; set; }
}
