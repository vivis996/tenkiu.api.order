using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

public class Order : DbModel<int>
{
  [Key]
  [Column("ID_Order", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("Delivery_season")]
  [StringLength(50)]
  public string DeliverySeason { get; set; }

  [Column("Delivery_Date")]
  public DateOnly DeliveryDate { get; set; }

  [Column("ID_Client", TypeName = "int(11)")]
  public int IdClient { get; set; }

  [Column("hash")]
  [StringLength(32)]
  public string Hash { get; set; }

  [InverseProperty("Order")]
  public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

  [InverseProperty("Order")]
  public virtual ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();

  [InverseProperty("Order")]
  public virtual ICollection<RelationOrderStatus> RelationOrderStatuses { get; set; } = new List<RelationOrderStatus>();

  [InverseProperty("Order")]
  public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
}
