using tenkiu.api.order.Models.Dto.OrderDetail;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.Order;

public class UpdateOrderDto : BaseOrderDto, IIdModel<int>
{
  public int Id { get; set; }
  public IEnumerable<UpdateOrderDetailDto> OrderDetails { get; set; }
}
