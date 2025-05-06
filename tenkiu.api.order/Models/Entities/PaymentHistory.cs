using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Payment_History")]
[Index("IdOrder", Name = "ID_Order")]
public class PaymentHistory : DbModel<int>
{
  [Key]
  [Column("ID_Payment_History", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Precision(10, 2)]
  public decimal Amount { get; set; }

  [Column("ID_Currency", TypeName = "int(11)")]
  public int IdCurrency { get; set; }

  [Column("ID_User", TypeName = "int(11)")]
  public int IdUser { get; set; }

  [Column("ID_Order", TypeName = "int(11)")]
  public int IdOrder { get; set; }

  [Column("Payment_Type", TypeName = "int(11)")]
  public PaymentType PaymentType { get; set; }

  [Column("Notes", TypeName = "varchar(255)")]
  public string? Notes { get; set; }

  [ForeignKey("IdOrder")]
  [InverseProperty("PaymentHistories")]
  public virtual Order Order { get; set; }
}
