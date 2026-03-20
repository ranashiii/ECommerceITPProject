using ECommOrderManagementData.Models;


namespace ECommerceITPProject
{

    internal static class DisplayFormatter
    {
        public static string FormatShippingType(ShippingType type) => type switch
        {
            ShippingType.Standard => "Standard (~5 days)",
            ShippingType.Express  => "Express (~2 days)",
            ShippingType.SameDay  => "Same Day (~8 hours)",
            _                     => type.ToString()
        };

        public static string FormatPaymentMethod(PaymentMethod method) => method switch
        {
            PaymentMethod.CashOnDelivery  => "Cash on Delivery (COD)",
            PaymentMethod.CreditDebitCard => "Credit / Debit Card",
            PaymentMethod.EWallet         => "E-Wallet",
            _                             => method.ToString()
        };

        public static string FormatOrderStatus(OrderStatus status) => status switch
        {
            OrderStatus.ToPay     => "To Pay — Awaiting payment confirmation",
            OrderStatus.ToShip    => "To Ship — Preparing your order",
            OrderStatus.InTransit => "In Transit",
            OrderStatus.ToReceive => "To Receive",
            OrderStatus.Completed => "Completed",
            OrderStatus.Cancelled => "Cancelled",
            _                     => status.ToString()
        };
    }
}
