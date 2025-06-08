using tenkiu.api.order.Models.Common;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Request;

public class BuyOrderSearchRequest : BasePagination<BuyOrderSearchOrderBy, Entities.BuyOrder>
{
  public int? IdStore { get; set; }
  public DateTimePeriod? PurchasePeriod { get; set; }
}

public enum BuyOrderSearchOrderBy
{
  None = 0,
  PurchaseDate = 1,
  DtCreated = 2,
}
