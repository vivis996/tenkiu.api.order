using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Relation_Order_Status")]
[Index("SellOrderId", Name = "ID_Order")]
[Index("IdStatusOrder", Name = "ID_Status_Order")]
public class SellOrderStatusRelation : DbModel<int>
{
  [Key]
  [Column("ID_Relation_Order_Status", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Order", TypeName = "int(11)")]
  public int SellOrderId { get; set; }

  [Column("ID_Status_Order", TypeName = "int(11)")]
  public OrderStatus IdStatusOrder { get; set; }

  [Column("Date_Relation")]
  public DateOnly DateRelation { get; set; }

  [ForeignKey("SellOrderId")]
  [InverseProperty("SellOrderStatusRelations")]
  public virtual SellOrder SellOrder { get; set; }

  [ForeignKey("IdStatusOrder")]
  [InverseProperty("RelationOrderStatuses")]
  public virtual StatusOrder StatusOrder { get; set; }
}
