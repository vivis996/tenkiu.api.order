using Microsoft.EntityFrameworkCore;
using tenkiu.api.order.Models.Entities;

namespace tenkiu.api.order.Context;

public interface IDbContext : vm.common.db.Context.IDbContext
{
  DbSet<ExchangeRate> ExchangeRates { get; set; }
  DbSet<Order> Orders { get; set; }
  DbSet<OrderDetail> OrderDetails { get; set; }
  DbSet<PaymentHistory> PaymentHistories { get; set; }
  DbSet<RelationOrderDetailsStatus> RelationOrderDetailsStatuses { get; set; }
  DbSet<RelationOrderStatus> RelationOrderStatuses { get; set; }
  DbSet<Shipping> Shippings { get; set; }
  DbSet<ShippingType> ShippingTypes { get; set; }
  DbSet<StatusOrder> StatusOrders { get; set; }
  DbSet<StatusOrderDetail> StatusOrderDetails { get; set; }
}
