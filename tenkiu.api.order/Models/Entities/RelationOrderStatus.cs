using System.ComponentModel.DataAnnotations;
using tenkiu.api.order.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Relation_Order_Status")]
[Index("IdOrder", Name = "ID_Order")]
[Index("IdStatusOrder", Name = "ID_Status_Order")]
public class RelationOrderStatus : DbModel<int>
{
  [Key]
  [Column("ID_Relation_Order_Status", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Order", TypeName = "int(11)")]
  public int IdOrder { get; set; }

  [Column("ID_Status_Order", TypeName = "int(11)")]
  public OrderStatus IdStatusOrder { get; set; }

  [Column("Date_Relation")]
  public DateOnly DateRelation { get; set; }

  [ForeignKey("IdOrder")]
  [InverseProperty("RelationOrderStatuses")]
  public virtual Order Order { get; set; }

  [ForeignKey("IdStatusOrder")]
  [InverseProperty("RelationOrderStatuses")]
  public virtual StatusOrder StatusOrder { get; set; }
}
