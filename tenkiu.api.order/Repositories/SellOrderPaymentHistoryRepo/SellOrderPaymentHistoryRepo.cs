using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.SellOrderPaymentHistoryRepo;

public class SellOrderPaymentHistoryRepo(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.SellOrderPaymentHistory, int>(dbContext, currentUserService, mapper), ISellOrderPaymentHistoryRepo
{
}
