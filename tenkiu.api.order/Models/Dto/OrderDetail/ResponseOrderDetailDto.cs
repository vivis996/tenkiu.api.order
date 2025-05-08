using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.OrderDetail;

public class ResponseOrderDetailDto : BaseOrderDetailDto, IIdModel<int>
{
  public int Id { get; set; }
}
