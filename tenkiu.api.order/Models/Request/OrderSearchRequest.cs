using vm.common.db.Models;

namespace tenkiu.api.order.Models.Request;

public class OrderSearchRequest : BasePagination<OrderSearchOrderBy, Entities.Order>
{
  public int? IdClient { get; set; }
}

public enum OrderSearchOrderBy
{
  None = 0,
  IdClient = 1,
  DtCreated = 2,
}
