using tenkiu.api.order.Models.Dto.SellOrderDetail;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.SellOrder;

public class UpdateSellOrderDto : BaseSellOrderDto, IIdModel<int>
{
  public int Id { get; set; }
  public IEnumerable<UpdateSellOrderDetailDto> OrderDetails { get; set; }
}
