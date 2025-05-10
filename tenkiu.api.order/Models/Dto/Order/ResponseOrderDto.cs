using tenkiu.api.order.Models.Dto.OrderDetail;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.Order;

public class ResponseOrderDto : BaseOrderDto, IIdModel<int>
{
  public int Id { get; set; }
  public string Hash { get; set; }
  public IEnumerable<ResponseOrderDetailDto> OrderDetails { get; set; }
}
