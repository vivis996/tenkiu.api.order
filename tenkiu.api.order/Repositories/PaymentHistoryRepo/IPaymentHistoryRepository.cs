using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.PaymentHistoryRepo;

public interface IPaymentHistoryRepository  : IRepository<Models.Entities.PaymentHistory, int>
{
}
