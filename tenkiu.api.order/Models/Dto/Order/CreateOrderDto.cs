using tenkiu.api.order.Models.Dto.OrderDetail;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.Order;

public class CreateOrderDto : BaseOrderDto, IDbCommon
{
  public IEnumerable<CreateOrderDetailDto> OrderDetails { get; set; }
}
