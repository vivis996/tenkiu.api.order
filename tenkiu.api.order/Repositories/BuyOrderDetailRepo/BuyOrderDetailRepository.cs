using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.BuyOrderDetailRepo;

public class BuyOrderDetailRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.BuyOrderDetail, int>(dbContext, currentUserService, mapper), IBuyOrderDetailRepository
{
}
