using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Status_Order")]
public class StatusOrder : DbModel<OrderStatus>
{
  [Key]
  [Column("ID_Status_Order", TypeName = "int(11)")]
  public override OrderStatus Id { get; set; }

  [StringLength(50)]
  public string Name { get; set; }

  [StringLength(200)]
  public string Description { get; set; }

  [InverseProperty("StatusOrder")]
  public virtual ICollection<SellOrderStatusRelation> RelationOrderStatuses { get; set; } = new List<SellOrderStatusRelation>();
}

