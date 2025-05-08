using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Buy_Order_Details")]
public class BuyOrderDetail : DbModel<int>
{
  /// <summary>
  /// Primary key of the purchase line item
  /// </summary>
  [Key]
  [Column("ID_Buy_Order_Details", TypeName = "int(11)")]
  public override int Id { get; set; }

  /// <summary>
  /// Foreign key referencing the parent purchase order
  /// </summary>
  [Column("ID_Buy_Order", TypeName = "int(11)")]
  public int BuyOrderId { get; set; }

  /// <summary>
  /// Foreign key referencing the product being purchased
  /// </summary>
  [Column("ID_Product", TypeName = "int(11)")]
  public int IdProduct { get; set; }

  /// <summary>
  /// Unit price at which the product was purchased
  /// </summary>
  [Column("Purchase_Price")]
  [Precision(10, 2)]
  public decimal PurchasePrice { get; set; }

  /// <summary>
  /// Tax amount applied to the purchase price
  /// </summary>
  [Column("Purchase_Price_Tax")]
  [Precision(10, 2)]
  public decimal PurchasePriceTax { get; set; }

  /// <summary>
  /// Currency code for the purchase price
  /// </summary>
  [Column("ID_Currency_Purchase", TypeName = "int(11)")]
  public int IdCurrencyPurchase { get; set; }
  
  /// <summary>
  /// Number of units bought.
  /// </summary>
  [Column(TypeName = "int(11)")]
  public int Quantity { get; set; }

  /// <summary>
  /// Total purchase price converted to the base currency
  /// </summary>
  [Column("Converted_Purchase_Price")]
  [Precision(18, 2)]
  public decimal ConvertedPurchasePrice { get; set; }

  /// <summary>
  /// Currency code in which the converted purchase amount is represented
  /// </summary>
  [Column("ID_Currency_Converted_Purchase", TypeName = "int(11)")]
  public int IdCurrencyConvertedPurchase { get; set; }

  /// <summary>
  /// Navigation property to the parent purchase order
  /// </summary>
  [ForeignKey("BuyOrderId")]
  [InverseProperty("BuyOrderDetails")]
  public virtual BuyOrder BuyOrder { get; set; }

  /// <summary>
  /// Collection of allocations connecting this purchase line to sales
  /// </summary>
  [InverseProperty("BuyOrderDetail")]
  public virtual ICollection<BuySellAllocation> BuySellAllocations { get; set; }
    = new List<BuySellAllocation>();
}
