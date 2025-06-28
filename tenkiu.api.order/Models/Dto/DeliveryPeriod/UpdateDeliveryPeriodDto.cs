using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.DeliveryPeriod;

public class UpdateDeliveryPeriodDto : BaseDeliveryPeriodDto, IIdModel<int>
{
  public int Id { get; set; }
}
