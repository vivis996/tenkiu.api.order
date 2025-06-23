using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Sell_Order_Payment_History")]
public class SellOrderPaymentHistory : DbModel<int>
{
  [Key]
  [Column("ID_Payment_History", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Precision(10, 2)]
  public decimal Amount { get; set; }

  [Column("ID_Currency", TypeName = "int(11)")]
  public int IdCurrency { get; set; }

  [Column("ID_Client", TypeName = "int(11)")]
  public int IdClient { get; set; }

  [Column("Payment_Direction", TypeName = "int(11)")]
  public PaymentDirection PaymentDirection { get; set; }

  [Column("Payment_Reason", TypeName = "int(11)")]
  public PaymentReason PaymentReason { get; set; }

  [Column("ID_Sell_Order", TypeName = "int(11)")]
  public int SellOrderId { get; set; }

  [Column("Payment_Type", TypeName = "int(11)")]
  public PaymentType PaymentType { get; set; }

  [Column("Notes", TypeName = "varchar(255)")]
  public string? Notes { get; set; }

  [Column("Payment_Date")]
  public DateOnly PaymentDate { get; set; }

  [ForeignKey("SellOrderId")]
  [InverseProperty("SellOrderPaymentHistories")]
  public virtual SellOrder SellOrder { get; set; }
}
