using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Order_Details")]
[Index("IdOrder", Name = "ID_Order")]
public class OrderDetail : DbModel<int>
{
  [Key]
  [Column("ID_Order_Details", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Order", TypeName = "int(11)")]
  public int IdOrder { get; set; }

  [Column("ID_Product", TypeName = "int(11)")]
  public int IdProduct { get; set; }

  [Column("Listed_Price")]
  [Precision(10, 2)]
  public decimal ListedPrice { get; set; }

  [Column("ID_Currency_Listed", TypeName = "int(11)")]
  public int IdCurrencyListed { get; set; }

  [Column("Purchase_Price")]
  [Precision(10, 2)]
  public decimal PurchasePrice { get; set; }

  [Column("Purchase_Price_Tax")]
  [Precision(10, 2)]
  public decimal PurchasePriceTax { get; set; }

  [Column("ID_Currency_Purchase", TypeName = "int(11)")]
  public int IdCurrencyPurchase { get; set; }

  [Column("Sell_Price")]
  [Precision(10, 2)]
  public decimal SellPrice { get; set; }

  [Column("ID_Currency_Sell", TypeName = "int(11)")]
  public int IdCurrencySell { get; set; }

  [Column(TypeName = "int(11)")]
  public int Quantity { get; set; }

  [ForeignKey("IdOrder")]
  [InverseProperty("OrderDetails")]
  public virtual Order Order { get; set; }

  [InverseProperty("OrderDetail")]
  public virtual ICollection<RelationOrderDetailsStatus> RelationOrderDetailsStatuses { get; set; } = new List<RelationOrderDetailsStatus>();
}
