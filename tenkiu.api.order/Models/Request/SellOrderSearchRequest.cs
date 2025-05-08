using vm.common.db.Models;

namespace tenkiu.api.order.Models.Request;

public class SellOrderSearchRequest : BasePagination<SellOrderSearchOrderBy, Entities.SellOrder>
{
  public int? IdClient { get; set; }
}

public enum SellOrderSearchOrderBy
{
  None = 0,
  IdClient = 1,
  DtCreated = 2,
}
