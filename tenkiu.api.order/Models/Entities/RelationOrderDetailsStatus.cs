using System.ComponentModel.DataAnnotations;
using tenkiu.api.order.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Relation_Order_Details_Status")]
[Index("IdOrderDetails", Name = "ID_Order_Details")]
[Index("IdStatusProduct", Name = "ID_Status_Product")]
public class RelationOrderDetailsStatus : DbModel<int>
{
  [Key]
  [Column("ID_Relation_ODS", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Order_Details", TypeName = "int(11)")]
  public int IdOrderDetails { get; set; }

  [Column("ID_Status_Product", TypeName = "int(11)")]
  public OrderStatusDetail IdStatusProduct { get; set; }

  [Column("Date_Relation")]
  public DateOnly DateRelation { get; set; }

  [ForeignKey("IdOrderDetails")]
  [InverseProperty("RelationOrderDetailsStatuses")]
  public virtual OrderDetail OrderDetail { get; set; }

  [ForeignKey("IdStatusProduct")]
  [InverseProperty("RelationOrderDetailsStatuses")]
  public virtual StatusOrderDetail StatusOrderDetail { get; set; }
}
