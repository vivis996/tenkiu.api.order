using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("BuySell_Allocation")]
public class BuySellAllocation : DbModel<int>
{
  [Key]
  [Column("ID_Allocation", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Buy_Order_Detail", TypeName = "int(11)")]
  public int BuyOrderDetailId { get; set; }

  [Column("ID_Sell_Order_Detail", TypeName = "int(11)")]
  public int SellOrderDetailId { get; set; }

  [Column("Quantity")]
  public int Quantity { get; set; }

  [ForeignKey("BuyOrderDetailId")]
  [InverseProperty("BuySellAllocations")]
  public virtual BuyOrderDetail BuyOrderDetail { get; set; }

  [ForeignKey("SellOrderDetailId")]
  [InverseProperty("BuySellAllocations")]
  public virtual SellOrderDetail SellOrderDetail { get; set; }
}
