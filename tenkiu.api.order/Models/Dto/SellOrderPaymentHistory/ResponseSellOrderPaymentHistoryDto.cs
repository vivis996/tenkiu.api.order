using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;

public class ResponseSellOrderPaymentHistoryDto : BaseSellOrderPaymentHistoryDto, IIdModel<int>
{
  public int Id { get; set; }
  public int SellOrderId { get; set; }
  public int IdClient { get; set; }
}
