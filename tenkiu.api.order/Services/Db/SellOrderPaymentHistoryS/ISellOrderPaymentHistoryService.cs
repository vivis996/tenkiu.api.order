using tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;
using tenkiu.api.order.Models.Entities;

namespace tenkiu.api.order.Services.Db.SellOrderPaymentHistoryS;

public interface ISellOrderPaymentHistoryService : IDisposable
{
  Task<Models.Entities.SellOrderPaymentHistory?> GetById(int id);
  Task<IEnumerable<Models.Entities.SellOrderPaymentHistory>> GetBySellOrderId(int sellOrderId);
  Task<Models.Entities.SellOrderPaymentHistory?> Create(CreateSellOrderPaymentHistoryDto value);
  Task<Models.Entities.SellOrderPaymentHistory?> Update(UpdateSellOrderPaymentHistoryDto value);
  Task<IEnumerable<PaymentDirectionRelation>> GetDirectionRelations();
  Task<IEnumerable<PaymentTypeDescription>> GetPaymentTypeDescriptions();
  Task<IEnumerable<BalanceDto>> GetBalanceBySellOrderId(int sellOrderId);
  Task<Dictionary<int, IEnumerable<BalanceDto>>> GetBalanceBySellOrderId(IEnumerable<int> sellOrderIds);
}
