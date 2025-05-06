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
