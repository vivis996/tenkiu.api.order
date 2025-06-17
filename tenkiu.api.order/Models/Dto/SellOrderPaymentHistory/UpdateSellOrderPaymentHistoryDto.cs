using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;

public class UpdateSellOrderPaymentHistoryDto : BaseSellOrderPaymentHistoryDto, IIdModel<int>
{
  public int Id { get; set; }
}
