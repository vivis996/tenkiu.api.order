using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Repositories.OrderRepo;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.SellOrderRepo;

public class SellOrderRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.SellOrder, int>(dbContext, currentUserService, mapper), ISellOrderRepository
{
}
