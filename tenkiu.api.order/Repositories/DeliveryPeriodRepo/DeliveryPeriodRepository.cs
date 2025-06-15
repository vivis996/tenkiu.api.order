using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.DeliveryPeriodRepo;

public class DeliveryPeriodRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.DeliveryPeriod, int>(dbContext, currentUserService, mapper), IDeliveryPeriodRepository
{
}
