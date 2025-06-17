using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;

public class CreateSellOrderPaymentHistoryDto : BaseSellOrderPaymentHistoryDto, IDbCommon
{
  public int SellOrderId { get; set; }
  public int IdClient { get; set; }
}
