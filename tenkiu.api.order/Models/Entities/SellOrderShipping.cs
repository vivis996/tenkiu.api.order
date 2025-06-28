using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Shipping")]
[Index("SellOrderId", Name = "ID_Order")]
public class SellOrderShipping : DbModel<int>
{
  [Key]
  [Column("ID_Shipping", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Order", TypeName = "int(11)")]
  public int SellOrderId { get; set; }

  [Column("ID_Shipping_Type", TypeName = "int(11)")]
  public int IdShippingType { get; set; }

  [Column("Guide_Number")]
  [StringLength(50)]
  public string? GuideNumber { get; set; }

  [Column("Date_Shipping")]
  public DateOnly? DateShipping { get; set; }

  [ForeignKey("SellOrderId")]
  [InverseProperty("SellOrderShippings")]
  public virtual SellOrder SellOrder { get; set; }
}
