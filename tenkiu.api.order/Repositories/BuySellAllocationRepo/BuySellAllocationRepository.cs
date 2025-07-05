using AutoMapper;
using tenkiu.api.order.Context;
using tenkiu.api.order.Services.CurrentUserService;
using vm.common.db.Repositories;

namespace tenkiu.api.order.Repositories.BuySellAllocationRepo;

public class BuySellAllocationRepository(IDbContext dbContext, ICurrentUserService currentUserService , IMapper mapper)
  : BaseRepository<Models.Entities.BuySellAllocation, int>(dbContext, currentUserService, mapper), IBuySellAllocationRepository
{
}
