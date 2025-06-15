using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Sell_Order")]
public class SellOrder : DbModel<int>
{
  [Key]
  [Column("ID_Sell_Order", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Delivery_Period", TypeName = "int(11)")]
  public int DeliveryPeriodId { get; set; }

  [Column("Delivery_Date")]
  public DateOnly DeliveryDate { get; set; }

  [Column("ID_Client", TypeName = "int(11)")]
  public int IdClient { get; set; }

  [Column("hash")]
  [StringLength(32)]
  public string Hash { get; set; }

  [Column("Base_Currency_Id", TypeName = "int(11)")]
  public int BaseCurrencyId { get; set; }

  [ForeignKey("DeliveryPeriodId")]
  [InverseProperty("SellOrders")]
  public virtual DeliveryPeriod DeliveryPeriod { get; set; }

  [InverseProperty("SellOrder")]
  public virtual ICollection<SellOrderDetail> SellOrderDetails { get; set; }
    = new List<SellOrderDetail>();

  [InverseProperty("SellOrder")]
  public virtual ICollection<SellOrderPaymentHistory> SellOrderPaymentHistories { get; set; }
    = new List<SellOrderPaymentHistory>();

  [InverseProperty("SellOrder")]
  public virtual ICollection<SellOrderStatusRelation> SellOrderStatusRelations { get; set; }
    = new List<SellOrderStatusRelation>();

  [InverseProperty("SellOrder")]
  public virtual ICollection<SellOrderShipping> SellOrderShippings { get; set; }
    = new List<SellOrderShipping>();
}
