using tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;
using tenkiu.api.order.Services.Db.SellOrderPaymentHistoryS;

namespace tenkiu.api.order.Models;

/// <summary>
/// Defines the types of payment methods supported by the order system.
/// </summary>
public enum PaymentType
{
  /// <summary>No payment method selected.</summary>
  None = 0,
  /// <summary>Payment made with cash.</summary>
  Cash = 1,
  /// <summary>Payment made via bank transfer.</summary>
  BankTransfer = 2,
  /// <summary>Payment made using credit card.</summary>
  CreditCard = 3,
  /// <summary>Payment made through PayPal.</summary>
  Paypal = 4,
  /// <summary>Payment made using an alternative method.</summary>
  Other = 6,
}

/// <summary>
/// Enumerates the reasons for payments in payment history.
/// </summary>
public enum PaymentReason
{
  /// <summary>
  /// No payment reason specified.
  /// </summary>
  None = 0,
  /// <summary>
  /// Money added to the account.
  /// </summary>
  AddMoney = 1,
  /// <summary>
  /// Money withdrawn from the account.
  /// </summary>
  WithdrawMoney = 2,
  /// <summary>
  /// Payment received for an order.
  /// </summary>
  OrderPayment = 3,
  /// <summary>
  /// Refund issued for an order.
  /// </summary>
  OrderRefund = 4,
  /// <summary>
  /// Disputed transaction reversed by the payment processor.
  /// </summary>
  Chargeback = 5,
  /// <summary>
  /// Platform or service fee charged.
  /// </summary>
  Fee = 6,
  /// <summary>
  /// Commission payout to partners or affiliates.
  /// </summary>
  Commission = 7,
  /// <summary>
  /// Manual adjustment to the account balance.
  /// </summary>
  Adjustment = 8,
  /// <summary>
  /// Interest credited or debited.
  /// </summary>
  Interest = 9,
  /// <summary>
  /// Internal funds transferred into the account.
  /// </summary>
  TransferIn = 10,
  /// <summary>
  /// Internal funds transferred out of the account.
  /// </summary>
  TransferOut = 11,
  /// <summary>
  /// Funds paid out to a vendor or merchant.
  /// </summary>
  Payout = 12,
  /// <summary>
  /// Generic reversal of a previous transaction.
  /// </summary>
  Reversal = 13
}

/// <summary>
/// Enumerates the various stages in the overall order lifecycle.
/// </summary>
public enum OrderStatus
{
  /// <summary>No order status method selected.</summary>
  None = 0,
  /// <summary>Order has been created in the system.</summary>
  Created = 1,
  /// <summary>Order initialization is complete and awaiting confirmation.</summary>
  Initialized = 2,
  /// <summary>Order has been confirmed by the customer or system.</summary>
  Confirmed = 3,
  /// <summary>Order is being processed and items are being prepared.</summary>
  Processing = 4,
  /// <summary>Order items have been packed and are ready for shipping.</summary>
  Packed = 5,
  /// <summary>Order has been shipped to the delivery address.</summary>
  Shipped = 6,
  /// <summary>Order is out for delivery with the carrier.</summary>
  OutForDelivery = 7,
  /// <summary>Order has been delivered to the recipient.</summary>
  Delivered = 8,
  /// <summary>Order has been cancelled and will not be fulfilled.</summary>
  Cancelled = 9,
  /// <summary>Order has been returned by the customer.</summary>
  Returned = 10,
  /// <summary>Order payment has been refunded.</summary>
  Refunded = 11,
}

/// <summary>
/// Enumerates the statuses for individual products within an order.
/// </summary>
public enum OrderStatusDetail
{
  /// <summary>No product status method selected.</summary>
  None = 0,
  /// <summary>Product is pending purchase.</summary>
  Pending = 1,
  /// <summary>Product purchase has been completed.</summary>
  Purchased = 2,
  /// <summary>Product is being prepared for shipment.</summary>
  InPreparation = 3,
  /// <summary>Product has been packaged.</summary>
  Packed = 4,
  /// <summary>Product is in transit to the destination.</summary>
  InTransit = 5,
  /// <summary>Product has been delivered to the recipient.</summary>
  Delivered = 6,
  /// <summary>Product order has been cancelled.</summary>
  Cancelled = 7,
  /// <summary>Product has been returned by the customer.</summary>
  Returned = 8,
  /// <summary>Product is on backorder due to stock unavailability.</summary>
  Backordered = 9,
}

/// <summary>
/// Classifies payment reasons as inflow or outflow.
/// </summary>
public enum PaymentDirection
{
  /// <summary>
  /// Represents no specific direction for the payment.
  /// </summary>
  None = 0,
  /// <summary>
  /// Represents money coming into the account
  /// </summary>
  Inflow = 1,
  /// <summary>
  /// Represents money going out of the account
  /// </summary>
  Outflow = 2,
}

/// <summary>
/// Extension methods for PaymentReason.
/// </summary>
public static class PaymentExtensions
{
  private static readonly Dictionary<PaymentDirection, IEnumerable<PaymentReason>> Directions = new()
  {
    { PaymentDirection.Inflow, [
        PaymentReason.AddMoney,
        PaymentReason.OrderPayment,
        PaymentReason.Commission,
        PaymentReason.Interest,
        PaymentReason.TransferIn,
        PaymentReason.Payout,
      ]
    },
    { PaymentDirection.Outflow, [
        PaymentReason.WithdrawMoney,
        PaymentReason.OrderRefund,
        PaymentReason.Chargeback,
        PaymentReason.Fee,
        PaymentReason.Adjustment,
        PaymentReason.TransferOut,
        PaymentReason.Reversal,
      ]
    }
  };

  public static IEnumerable<PaymentDirectionRelation> GetDirectionRelations()
  {
    return Directions.Select(d => new PaymentDirectionRelation
    {
      Direction = d.Key,
      Description = d.Key.ToString(),
      Reasons = d.Value.Select(r => new PaymentReasonDescription
      {
        Reason = r,
        Description = r.ToString(),
      }),
    });
  }

  /// <summary>
  /// Gets the direction (inflow or outflow) for a given payment reason.
  /// </summary>
  public static PaymentDirection GetDirection(this PaymentReason reason)
  {
    var direction = Directions.FirstOrDefault(d => d.Value.Contains(reason)).Key;
    return direction != default ? direction : throw new ArgumentException($"Unknown payment reason: {reason}");
  }
  
  public static Dictionary<PaymentDirection, IEnumerable<PaymentReason>> GetDirections()
  {
    return Directions;
  }
  
  public static IEnumerable<PaymentTypeDescription> GetPaymentTypes()
  {
    return Enum.GetValues<PaymentType>()
      .Where(type => type != PaymentType.None)
      .Select(type => new PaymentTypeDescription
      {
        Type = type,
        Description = type.ToString(),
      });
  }
}
