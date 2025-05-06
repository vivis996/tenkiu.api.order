using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Status_Order_Detail")]
public class StatusOrderDetail : DbModel<OrderStatusDetail>
{
  [Key]
  [Column("ID_Status_Order_Detail", TypeName = "int(11)")]
  public override OrderStatusDetail Id { get; set; }

  [StringLength(50)]
  public string Name { get; set; }

  [StringLength(200)]
  public string Description { get; set; }

  [InverseProperty("StatusOrderDetail")]
  public virtual ICollection<RelationOrderDetailsStatus> RelationOrderDetailsStatuses { get; set; } = new List<RelationOrderDetailsStatus>();
}
