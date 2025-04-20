using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Shipping_Types")]
public class ShippingType : DbModel<int>
{
  [Key]
  [Column("ID_Shipping_Type", TypeName = "int(11)")]
  public override int Id { get; set; }

  [StringLength(50)]
  public string Name { get; set; }

  [Column("Web_site")]
  [StringLength(255)]
  public string? WebSite { get; set; }
}
