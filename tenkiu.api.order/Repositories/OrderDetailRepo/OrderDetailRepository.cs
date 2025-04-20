using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.OrderDetailRepo;

public class OrderDetailRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.OrderDetail, int>(dbContext, currentUserService, mapper), IOrderDetailRepository
{
}
