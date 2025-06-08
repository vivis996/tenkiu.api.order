using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Relation_Order_Details_Status")]
[Index("SellOrderDetailId", Name = "ID_Order_Details")]
[Index("StatusOrderDetailId", Name = "ID_Status_Product")]
public class RelationOrderDetailsStatus : DbModel<int>
{
  [Key]
  [Column("ID_Relation_ODS", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Order_Details", TypeName = "int(11)")]
  public int SellOrderDetailId { get; set; }

  [Column("ID_Status_Product", TypeName = "int(11)")]
  public OrderStatusDetail StatusOrderDetailId { get; set; }

  [Column("Date_Relation")]
  public DateOnly DateRelation { get; set; }

  [ForeignKey("SellOrderDetailId")]
  [InverseProperty("RelationOrderDetailsStatuses")]
  public virtual SellOrderDetail SellOrderDetail { get; set; }

  [ForeignKey("StatusOrderDetailId")]
  [InverseProperty("RelationOrderDetailsStatuses")]
  public virtual StatusOrderDetail StatusOrderDetail { get; set; }
}
