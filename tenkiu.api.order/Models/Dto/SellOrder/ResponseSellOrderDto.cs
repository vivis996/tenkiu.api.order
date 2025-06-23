using tenkiu.api.order.Models.Dto.SellOrderDetail;
using tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Dto.SellOrder;

public class ResponseSellOrderDto : BaseSellOrderDto, IIdModel<int>
{
  public int Id { get; set; }
  public string Hash { get; set; }
  public decimal Balance { get; set; }
  public IEnumerable<ResponseSellOrderDetailDto> OrderDetails { get; set; }
  public IEnumerable<BalanceDto> Balances { get; set; }
}
