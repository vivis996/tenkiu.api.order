using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.SellOrderDetail;

public class UpdateSellOrderDetailDto : BaseSellOrderDetailDto, IIdModel<int>
{
  public int Id { get; set; }
  public int SellOrderId { get; set; }
  public int BuyOrderDetailId { get; set; }
}
