using System.Data;
using Microsoft.EntityFrameworkCore;
using tenkiu.api.order.Models.Entities;
using tenkiu.api.order.Models;

namespace tenkiu.api.order.Context;

public partial class MyDbContext : DbContext, IDbContext
{
  private readonly IDbConnection _dbConnection;

  public MyDbContext(IDbConnection dbConnection)
  {
    this._dbConnection = dbConnection;
  }

  public MyDbContext(DbContextOptions<MyDbContext> options, IDbConnection dbConnection)
    : base(options)
  {
    this._dbConnection = dbConnection;
  }

  public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

  public virtual DbSet<SellOrder> SellOrders { get; set; }
  public virtual DbSet<BuyOrder> BuyOrders { get; set; }
  public virtual DbSet<SellOrderDetail> SellOrderDetails { get; set; }
  public virtual DbSet<BuyOrderDetail> BuyOrderDetails { get; set; }
  public virtual DbSet<BuySellAllocation> BuySellAllocations { get; set; }
  public virtual DbSet<SellOrderPaymentHistory> SellOrderPaymentHistories { get; set; }

  public virtual DbSet<RelationOrderDetailsStatus> RelationOrderDetailsStatuses { get; set; }
  public virtual DbSet<SellOrderStatusRelation> SellOrderStatusRelations { get; set; }
  public virtual DbSet<SellOrderShipping> SellOrderShippings { get; set; }

  public virtual DbSet<ShippingType> ShippingTypes { get; set; }

  public virtual DbSet<StatusOrder> StatusOrders { get; set; }

  public virtual DbSet<StatusOrderDetail> StatusOrderDetails { get; set; }
  public virtual DbSet<DeliveryPeriod> DeliveryPeriods { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseMySql(this._dbConnection.ConnectionString,
                            ServerVersion.AutoDetect(this._dbConnection.ConnectionString));
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ExchangeRate>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");
    });

    modelBuilder.Entity<SellOrder>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");
    });

    modelBuilder.Entity<SellOrderDetail>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");

      entity.HasOne(d => d.SellOrder).WithMany(p => p.SellOrderDetails).HasConstraintName("Sell_Order_Details_ibfk_1");
    });

    modelBuilder.Entity<SellOrderPaymentHistory>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");

      entity.HasOne(d => d.SellOrder).WithMany(p => p.SellOrderPaymentHistories).HasConstraintName("Payment_History_ibfk_1");
    });

    modelBuilder.Entity<RelationOrderDetailsStatus>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");

      entity.HasOne(d => d.SellOrderDetail).WithMany(p => p.RelationOrderDetailsStatuses).HasConstraintName("Relation_Order_Details_Status_ibfk_1");

      entity.HasOne(d => d.StatusOrderDetail).WithMany(p => p.RelationOrderDetailsStatuses).HasConstraintName("Relation_Order_Details_Status_ibfk_2");
    });

    modelBuilder.Entity<SellOrderStatusRelation>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");

      entity.HasOne(d => d.SellOrder).WithMany(p => p.SellOrderStatusRelations).HasConstraintName("Relation_Order_Status_ibfk_1");

      entity.HasOne(d => d.StatusOrder).WithMany(p => p.RelationOrderStatuses).HasConstraintName("Relation_Order_Status_ibfk_2");
      
      entity.HasOne(r => r.StatusOrder)
          .WithMany(s => s.RelationOrderStatuses)
          .HasForeignKey(r => r.IdStatusOrder)
          .HasPrincipalKey(s => s.Id);
    });

    modelBuilder.Entity<SellOrderShipping>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");

      entity.HasOne(d => d.SellOrder).WithMany(p => p.SellOrderShippings).HasConstraintName("Shipping_ibfk_1");
    });

    modelBuilder.Entity<ShippingType>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");

      entity.Property(e => e.Id);
    });

    modelBuilder.Entity<StatusOrder>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");

      entity.Property(e => e.Id);
    });

    modelBuilder.Entity<StatusOrderDetail>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");

      entity.Property(e => e.Id);
    });

    modelBuilder.Entity<BuyOrder>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");
    });

    modelBuilder.Entity<BuyOrderDetail>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.HasOne(d => d.BuyOrder)
            .WithMany(p => p.BuyOrderDetails)
            .HasConstraintName("Buy_Order_Details_ibfk_1");
    });

    modelBuilder.Entity<BuySellAllocation>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.HasOne(d => d.BuyOrderDetail)
            .WithMany(p => p.BuySellAllocations)
            .HasConstraintName("BuySell_Allocation_ibfk_1");
      entity.HasOne(d => d.SellOrderDetail)
            .WithMany(p => p.BuySellAllocations)
            .HasConstraintName("BuySell_Allocation_ibfk_2");
    });

    modelBuilder.Entity<DeliveryPeriod>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PRIMARY");

      entity.Property(e => e.Id);
      entity.Property(e => e.CreatedDt).HasDefaultValueSql("current_timestamp()");

      entity.HasMany(d => d.SellOrders)
            .WithOne(p => p.DeliveryPeriod)
            .HasForeignKey(p => p.DeliveryPeriodId)
            .HasConstraintName("Delivery_Periods_ibfk_1");
    });

    this.InitSeedValues(modelBuilder);

    OnModelCreatingPartial(modelBuilder);
  }

  private void InitSeedValues(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<StatusOrder>().HasData(
        new StatusOrder { Id = OrderStatus.Created, Name = nameof(OrderStatus.Created), Description = "Order has been created in the system.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.Initialized, Name = nameof(OrderStatus.Initialized), Description = "Order initialization is complete and awaiting confirmation.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.Confirmed, Name = nameof(OrderStatus.Confirmed), Description = "Order has been confirmed by the customer or system.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.Processing, Name = nameof(OrderStatus.Processing), Description = "Order is being processed and items are being prepared.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.Packed, Name = nameof(OrderStatus.Packed), Description = "Order items have been packed and are ready for shipping.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.Shipped, Name = nameof(OrderStatus.Shipped), Description = "Order has been shipped to the delivery address.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.OutForDelivery, Name = nameof(OrderStatus.OutForDelivery), Description = "Order is out for delivery with the carrier.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.Delivered, Name = nameof(OrderStatus.Delivered), Description = "Order has been delivered to the recipient.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.Cancelled, Name = nameof(OrderStatus.Cancelled), Description = "Order has been cancelled and will not be fulfilled.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.Returned, Name = nameof(OrderStatus.Returned), Description = "Order has been returned by the customer.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrder { Id = OrderStatus.Refunded, Name = nameof(OrderStatus.Refunded), Description = "Order payment has been refunded.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, }
    );
  
    modelBuilder.Entity<StatusOrderDetail>().HasData(
        new StatusOrderDetail { Id = OrderStatusDetail.Pending, Name = nameof(OrderStatusDetail.Pending), Description = "Product is pending purchase.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrderDetail { Id = OrderStatusDetail.Purchased, Name = nameof(OrderStatusDetail.Purchased), Description = "Product purchase has been completed.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrderDetail { Id = OrderStatusDetail.InPreparation, Name = nameof(OrderStatusDetail.InPreparation), Description = "Product is being prepared for shipment.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrderDetail { Id = OrderStatusDetail.Packed, Name = nameof(OrderStatusDetail.Packed), Description = "Product has been packaged.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrderDetail { Id = OrderStatusDetail.InTransit, Name = nameof(OrderStatusDetail.InTransit), Description = "Product is in transit to the destination.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrderDetail { Id = OrderStatusDetail.Delivered, Name = nameof(OrderStatusDetail.Delivered), Description = "Product has been delivered to the recipient.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrderDetail { Id = OrderStatusDetail.Cancelled, Name = nameof(OrderStatusDetail.Cancelled), Description = "Product order has been cancelled.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrderDetail { Id = OrderStatusDetail.Returned, Name = nameof(OrderStatusDetail.Returned), Description = "Product has been returned by the customer.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, },
        new StatusOrderDetail { Id = OrderStatusDetail.Backordered, Name = nameof(OrderStatusDetail.Backordered), Description = "Product is on backorder due to stock unavailability.", CreatedBy = 0, CreatedDt = DateTime.UtcNow, }
    );
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
