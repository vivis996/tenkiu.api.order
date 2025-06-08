using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.SellOrderDetailRepo;

public class SellOrderDetailRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.SellOrderDetail, int>(dbContext, currentUserService, mapper), ISellOrderDetailRepository
{
}
