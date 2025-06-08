using Microsoft.EntityFrameworkCore;
using tenkiu.api.order.Models.Entities;

namespace tenkiu.api.order.Context;

public interface IDbContext : vm.common.db.Context.IDbContext
{
  DbSet<ExchangeRate> ExchangeRates { get; set; }
  DbSet<SellOrder> SellOrders { get; set; }
  DbSet<BuyOrder> BuyOrders { get; set; }
  DbSet<SellOrderDetail> SellOrderDetails { get; set; }
  DbSet<BuyOrderDetail> BuyOrderDetails { get; set; }
  DbSet<BuySellAllocation> BuySellAllocations { get; set; }
  DbSet<SellOrderPaymentHistory> SellOrderPaymentHistories { get; set; }
  DbSet<RelationOrderDetailsStatus> RelationOrderDetailsStatuses { get; set; }
  DbSet<SellOrderStatusRelation> SellOrderStatusRelations { get; set; }
  DbSet<SellOrderShipping> SellOrderShippings { get; set; }
  DbSet<ShippingType> ShippingTypes { get; set; }
  DbSet<StatusOrder> StatusOrders { get; set; }
  DbSet<StatusOrderDetail> StatusOrderDetails { get; set; }
}
