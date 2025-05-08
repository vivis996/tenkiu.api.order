using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Buy_Order")]
public class BuyOrder : DbModel<int>
{
  /// <summary>
  /// Primary key of the purchase order
  /// </summary>
  [Key]
  [Column("ID_Buy_Order", TypeName = "int(11)")]
  public override int Id { get; set; }

  /// <summary>
  /// Foreign key referencing the store where the purchase was made
  /// </summary>
  [Column("ID_Store", TypeName = "int(11)")]
  public int IdStore { get; set; }

  /// <summary>
  /// Date the purchase order was created
  /// </summary>
  [Column("Purchase_Date")]
  public DateOnly PurchaseDate { get; set; }

  /// <summary>
  /// Base currency used for this purchase order
  /// </summary>
  [Column("Base_Currency_Id", TypeName = "int(11)")]
  public int BaseCurrencyId { get; set; }

  /// <summary>
  /// Currency to which the purchase price is converted
  /// </summary>
  [Column("Converted_Currency_Id", TypeName = "int(11)")]
  public int ConvertedCurrencyId { get; set; }

  /// <summary>
  /// Exchange rate used to convert purchase currency to base currency
  /// </summary>
  [Column("Exchange_Rate")]
  [Precision(18, 8)]
  public decimal ExchangeRate { get; set; }

  /// <summary>
  /// Collection of detail lines associated with this purchase order
  /// </summary>
  [InverseProperty("BuyOrder")]
  public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; set; }
    = new List<BuyOrderDetail>();
}
