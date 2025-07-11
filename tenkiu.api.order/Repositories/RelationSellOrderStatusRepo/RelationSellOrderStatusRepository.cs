using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.RelationSellOrderStatusRepo;

public class RelationSellOrderStatusRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.SellOrderStatusRelation, int>(dbContext, currentUserService, mapper), IRelationSellOrderStatusRepository
{
}
