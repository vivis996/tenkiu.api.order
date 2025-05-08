using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.OrderDetail;

public class UpdateOrderDetailDto : BaseOrderDetailDto, IIdModel<int>
{
  public int Id { get; set; }
}
