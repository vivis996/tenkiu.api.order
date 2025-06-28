using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using vm.common.db.Models;

namespace tenkiu.api.order.Models.Entities;

[Table("Sell_Order_Details")]
public class SellOrderDetail : DbModel<int>
{
  [Key]
  [Column("ID_Sell_Order_Details", TypeName = "int(11)")]
  public override int Id { get; set; }

  [Column("ID_Sell_Order", TypeName = "int(11)")]
  public int SellOrderId { get; set; }

  [Column("ID_Product", TypeName = "int(11)")]
  public int IdProduct { get; set; }

  [Column("Sell_Price")]
  [Precision(10, 2)]
  public decimal SellPrice { get; set; }

  [Column("ID_Currency_Sell", TypeName = "int(11)")]
  public int IdCurrencySell { get; set; }
  
  /// <summary>
  /// Number of units sold.
  /// </summary>
  [Column(TypeName = "int(11)")]
  public int Quantity { get; set; }

  [ForeignKey("SellOrderId")]
  [InverseProperty("SellOrderDetails")]
  public virtual SellOrder SellOrder { get; set; }
  
  [InverseProperty("SellOrderDetail")]
  public virtual ICollection<RelationOrderDetailsStatus> RelationOrderDetailsStatuses { get; set; } = new List<RelationOrderDetailsStatus>();

  [InverseProperty("SellOrderDetail")]
  public virtual ICollection<BuySellAllocation> BuySellAllocations { get; set; }
      = new List<BuySellAllocation>();
}
