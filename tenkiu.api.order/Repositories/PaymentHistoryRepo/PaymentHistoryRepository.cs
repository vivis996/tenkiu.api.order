using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.PaymentHistoryRepo;

public class PaymentHistoryRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.PaymentHistory, int>(dbContext, currentUserService, mapper), IPaymentHistoryRepository
{
}
