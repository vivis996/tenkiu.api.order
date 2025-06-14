using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.DeliveryPeriod;

public class ResponseDeliveryPeriodDto : BaseDeliveryPeriodDto, IIdModel<int>
{
  public int Id { get; set; }
}
