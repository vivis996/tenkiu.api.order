using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.RelationOrderDetailsStatusRepo;

public class RelationOrderDetailsStatusRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.RelationOrderDetailsStatus, int>(dbContext, currentUserService, mapper), IRelationOrderDetailsStatusRepository
{
}
