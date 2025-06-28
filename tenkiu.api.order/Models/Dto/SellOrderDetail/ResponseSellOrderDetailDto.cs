using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.SellOrderDetail;

public class ResponseSellOrderDetailDto : BaseSellOrderDetailDto, IIdModel<int>
{
  public int Id { get; set; }
}
