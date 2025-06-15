using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Delivery_Periods")]
public class DeliveryPeriod : DbModel<int>
{
  [Key]
  [Column("ID_Delivery_Period", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("Period_Name", TypeName = "varchar(100)")]
  [StringLength(100)]
  public string PeriodName { get; set; }

  [Column("Is_Active")]
  public bool IsActive { get; set; }

  [Column("Start_Date")]
  public DateOnly StartDate { get; set; }

  [Column("End_Date")]
  public DateOnly EndDate { get; set; }
  
  [InverseProperty("DeliveryPeriod")]
  public virtual ICollection<SellOrder> SellOrders { get; set; } = new List<SellOrder>();
}
