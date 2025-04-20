using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.RelationOrderStatusRepo;

public class RelationOrderStatusRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.RelationOrderStatus, int>(dbContext, currentUserService, mapper), IRelationOrderStatusRepository
{
}
