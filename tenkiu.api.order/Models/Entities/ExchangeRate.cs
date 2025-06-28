using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Exchange_Rate")]
public class ExchangeRate : DbModel<int>
{
  [Key]
  [Column("ID_Exchange_Rate", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Currency_Origen", TypeName = "int(11)")]
  public int IdCurrencyOrigen { get; set; }

  [Column("ID_Currency_Destination", TypeName = "int(11)")]
  public int IdCurrencyDestination { get; set; }

  [Column("Exchange_Rate")]
  [Precision(10, 4)]
  public decimal ExchangeRate1 { get; set; }
}
