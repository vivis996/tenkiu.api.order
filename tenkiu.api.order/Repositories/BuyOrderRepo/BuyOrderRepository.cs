using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.BuyOrderRepo;

public class BuyOrderRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.BuyOrder, int>(dbContext, currentUserService, mapper), IBuyOrderRepository
{
}
