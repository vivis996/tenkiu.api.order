using tenkiu.api.order.Models.Common;

namespace tenkiu.api.order.Models.Dto.DeliveryPeriod;

public class BaseDeliveryPeriodDto
{
  public string PeriodName { get; set; }
  public DateTimePeriod Period { get; set; }
  public bool IsActive { get; set; }
}
