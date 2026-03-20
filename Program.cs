/* PROJECT IN INTEGRATIVE PROGRAMMING AND TECHNOLOGY
 * ECOMMERCE APP (e.g. Lazada, Shopee, Amazon, etc.)
 * 
 * ASSINGED FEATURE: ORDER MANAGEMENT
 * WHERE IN:
 *  1. AFTER A COSTUMER TRIES TO CHECK OUT IN THE SHOPPING CART; THEY WILL BE REDIRECTED TO THE ORDER MANAGEMENT PAGE
 *  2. IN THE ORDER MANAGEMENT PAGE, THEY CAN SEE THEIR ORDER DETAILS, SUCH AS...
 *     A. SELECTING A PRESET DELIVERY ADDRESS AMONG THE CHOICES
 *          - OPTION A (LABELED AS DEFAULT) FIELDS: FULLNAME; PHONE NUMBER; ADDRESS (STREETNAME/BUILDING/HOUSE NUMBER, CITY, PROVINCE, REGION, POSTAL CODE)
 *          - OPTION B (WORK OR HOME) FIELDS: FULLNAME; PHONE NUMBER; ADDRESS (STREETNAME/BUILDING/HOUSE NUMBER, CITY, PROVINCE, REGION, POSTAL CODE)
 *          - OPTION XYZ...
 *    B. STORE SELLER NAME
 *    C. ITEM DISPLAY (ITEM NAME, ITEM IMAGE, ITEM PRICE, ITEM QUANTITY, TOTAL ITEM AMOUNT)
 *    D. SHOP/PLATFORM VOUCHER/S (OPTIONAL FOR NOW)
 *    E. ERECIPET REQUEST (OPTIONAL FOR NOW)
 *    F. SHIPPING OPTION 
 *          - DOOR TO DOOR DELIVERY (DEFAULT)
 *          - PICK UP POINT (OPTIONAL FOR NOW)
 *          - DELIVERY TYPE (e.g. STANDARD, EXPRESS, SAME DAY, etc.)
 *          - ESTIMATED DATE/TIME OF DELIVERY
 *    G. TOTAL NUMBER OF ITEMS
 *    H. PAYMENT METHOD
 *          - OPTION A (CASH ON DELIVERY)
 *          - OPTION B (CREDIT/DEBIT CARD)
 *          - OPTION C (E-WALLET)
 *    I. PAYMENT DETAILS/BREAKDOWN
 *          - MERCHANDISE SUBTOTAL
 *          - SHIPPING SUBTOTAL
 *          - DISCOUNTS (IF ANY)
 *          - TOTAL PAYMENT AMOUNT
 *    J. PLACE ORDER BUTTON  
 *    K. ORDER ID (AFTER PLACING THE ORDER)
 *    L. SHIPPING ID / TRACKING ID (AFTER PLACING THE ORDER)
 *    L. ORDER STATUS (AFTER PLACING THE ORDER, OPTIONAL FOR NOW)
 *          - TO PAY (DEPENDS ON THE CONDITION)
 *          - TO SHIP
 *          - IN TRANSIT 
 *          - TO RECEIVE
 *          - COMPLETED
 *          - CANCELLED
 */

//!!! TO ADD !!!
//    - AUTO PHP Currency
//    - SELLER STORE  
using ECommOrderManagementData.Models;
using ECommOrderManagementAppService;

namespace ECommerceITPProject
{
    internal class Program
    {

        private static readonly OrderAppService _orderAppService = new OrderAppService();

        static void Main(string[] args)
        {
            PrintHeader();

            // ==== STEP 1: Item Selection 
            PrintSectionHeader("STEP 1 — SELECT ITEMS");
            DisplayItemCatalog();
            List<string> selectedCodes = PromptItemSelection();

            if (selectedCodes.Count == 0)
            {
                Console.WriteLine("\n  No items selected. Exiting order process.");
                return;
            }

            List<OrderItem> orderItems = _orderAppService.BuildOrderItems(selectedCodes);

            // ==== STEP 2: Address Selection 
            PrintSectionHeader("STEP 2 — SELECT DELIVERY ADDRESS");
            Address selectedAddress = PromptAddressSelection();

            // ==== STEP 3: Shipping Option 
            PrintSectionHeader("STEP 3 — SELECT SHIPPING OPTION");
            ShippingType shippingType = PromptShippingSelection();

            // ==== STEP 4: Payment Method 
            PrintSectionHeader("STEP 4 — SELECT PAYMENT METHOD");
            PaymentMethod paymentMethod = PromptPaymentSelection();

            // ==== STEP 5: Order Summary 
            PrintSectionHeader("STEP 5 — ORDER SUMMARY");
            DisplayOrderSummary(orderItems, selectedAddress, shippingType, paymentMethod);

            // ==== STEP 6: Place Order 
            if (!PromptConfirmOrder())
            {
                Console.WriteLine("\n  Order cancelled. Returning to cart.");
                return;
            }

            Order placedOrder = _orderAppService.PlaceOrder(
                selectedAddress, orderItems, shippingType, paymentMethod);

            // ==== STEP 7: Order Confirmation 
            PrintSectionHeader("ORDER PLACED SUCCESSFULLY!");
            DisplayOrderConfirmation(placedOrder);

            Console.WriteLine("\n  Press any key to exit...");
            Console.ReadKey();
        }

        // uiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii
        static void PrintHeader()
        {
            Console.WriteLine("============== ECOMMERCE SYSTEM | ORDER MANAGEMENT PAGE: BUYER'S END ==============");
            Console.WriteLine("\tversion: 0.3 | separation of concerns - mid implementation\n");
        }

        static void PrintSectionHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine($"  ┌─── {title} {"─".PadRight(Math.Max(0, 50 - title.Length), '─')}┐");
            Console.WriteLine();
        }

        static void DisplayItemCatalog()
        {
            var catalog = _orderAppService.GetItemCatalog();
            Console.WriteLine("  Available Items:");
            Console.WriteLine("  {0,-5} {1,-15} {2}", "Code", "Item Name", "Price");
            Console.WriteLine("  " + new string('-', 35));
            foreach (var entry in catalog)
            {
                Console.WriteLine("  [{0}]  {1,-15} PHP {2:F2}", entry.Key, entry.Value.Name, entry.Value.Price);
            }
        }

        static void DisplayOrderSummary(
            List<OrderItem> items,
            Address address,
            ShippingType shippingType,
            PaymentMethod paymentMethod)
        {
            double merch = _orderAppService.CalculateMerchandiseSubtotal(items);
            double shipping = _orderAppService.GetShippingFee(shippingType);
            DateTime eta = _orderAppService.GetEstimatedDelivery(shippingType);

            // Delivery address
            Console.WriteLine("  Delivery Address:");
            Console.WriteLine($"    [{address.Label}] {address.FullName}  |  {address.PhoneNumber}");
            Console.WriteLine($"    {address.GetFormattedAddress()}");

            // Items
            Console.WriteLine();
            Console.WriteLine($"  Seller: Sample Store");
            Console.WriteLine();
            Console.WriteLine("  {0,-6} {1,-15} {2,5}   {3,10}   {4,12}",
                "Code", "Item", "Qty", "Unit Price", "Total");
            Console.WriteLine("  " + new string('-', 55));

            foreach (var item in items)
            {
                Console.WriteLine("  {0,-6} {1,-15} {2,5}   PHP {3,7:F2}   PHP {4,7:F2}",
                    item.ItemCode, item.ItemName, item.Quantity, item.UnitPrice, item.TotalAmount);
            }

            Console.WriteLine("  " + new string('-', 55));
            Console.WriteLine($"  Total Items: {items.Sum(i => i.Quantity)}");

            // Ship
            Console.WriteLine();
            Console.WriteLine($"  Shipping Option  : Door to Door Delivery");
            Console.WriteLine($"  Delivery Type    : {FormatShippingType(shippingType)}");
            Console.WriteLine($"  Estimated Arrival: {eta:ddd, MMM dd yyyy}{(shippingType == ShippingType.SameDay ? $" by {eta:hh:mm tt}" : "")}");

            // Pay
            Console.WriteLine();
            Console.WriteLine($"  Payment Method: {FormatPaymentMethod(paymentMethod)}");

            // Pay Receipt
            Console.WriteLine();
            Console.WriteLine("  ── Payment Breakdown ──────────────────────");
            Console.WriteLine($"  Merchandise Subtotal  : PHP {merch,10:F2}");
            Console.WriteLine($"  Shipping Fee          : PHP {shipping,10:F2}");
            Console.WriteLine($"  Voucher / Discount    : PHP {0.00,10:F2}");
            Console.WriteLine("  " + new string('─', 42));
            Console.WriteLine($"  TOTAL PAYMENT         : PHP {merch + shipping,10:F2}");
        }

        static void DisplayOrderConfirmation(Order order)
        {
            Console.WriteLine($"  Order ID   : {order.OrderID}");
            Console.WriteLine($"  Tracking ID: {order.TrackingID}");
            Console.WriteLine($"  Order Date : {order.OrderDate:MMM dd, yyyy  hh:mm tt}");
            Console.WriteLine($"  Status     : {FormatOrderStatus(order.Status)}");
            Console.WriteLine();
            Console.WriteLine($"  Estimated Delivery: {order.EstimatedDelivery:ddd, MMM dd yyyy}");
            Console.WriteLine();
            Console.WriteLine("  Thank you for your order! Keep your Order ID for tracking.");
        }

        // inpuuuuuuuuuuttttttttttttttttttttttttttt

        static List<string> PromptItemSelection()
        {
            var selected = new List<string>();
            Console.WriteLine();
            Console.WriteLine("  Enter item codes one at a time (A / B / C / D).");
            Console.WriteLine("  Add the same code again to increase quantity.");
            Console.WriteLine("  Type 'DONE' when finished.\n");

            while (true)
            {
                Console.Write("  >> Item Code: ");
                string input = Console.ReadLine()?.Trim().ToUpper() ?? "";

                if (input == "DONE") break;

                if (_orderAppService.IsValidItemCode(input))
                {
                    selected.Add(input);
                    Console.WriteLine($"     ✓ {input} added. (Current count: {selected.Count(c => c == input)}x {input})\n");
                }
                else
                {
                    Console.WriteLine("     ✗ Invalid code. Please enter A, B, C, or D.\n");
                }
            }

            Console.WriteLine($"\n  Selection complete. {selected.Count} item(s) in cart.");
            return selected;
        }

        static Address PromptAddressSelection()
        {
            var addresses = _orderAppService.GetSavedAddresses();

            Console.WriteLine("  Your saved addresses:\n");
            for (int i = 0; i < addresses.Count; i++)
            {
                var a = addresses[i];
                Console.WriteLine($"  [{i + 1}] {a.Label}");
                Console.WriteLine($"      {a.FullName}  |  {a.PhoneNumber}");
                Console.WriteLine($"      {a.GetFormattedAddress()}");
                Console.WriteLine();
            }

            while (true)
            {
                Console.Write($"  >> Select address (1–{addresses.Count}): ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (int.TryParse(input, out int choice) && _orderAppService.IsValidAddressIndex(choice))
                {
                    Address selected = _orderAppService.GetAddressByIndex(choice);
                    Console.WriteLine($"\n  ✓ Address selected: [{selected.Label}] {selected.GetFormattedAddress()}");
                    return selected;
                }

                Console.WriteLine($"  ✗ Invalid selection. Enter a number from 1 to {addresses.Count}.\n");
            }
        }

        static ShippingType PromptShippingSelection()
        {
            Console.WriteLine("  Shipping Method : Door to Door Delivery (default)\n");
            Console.WriteLine("  Select delivery type:");
            Console.WriteLine("  [1] Standard  — ~5 days  | PHP  50.00");
            Console.WriteLine("  [2] Express   — ~2 days  | PHP 100.00");
            Console.WriteLine("  [3] Same Day  — ~8 hours | PHP 150.00");

            while (true)
            {
                Console.Write("\n  >> Delivery Type (1/2/3): ");
                string input = Console.ReadLine()?.Trim() ?? "";

                switch (input)
                {
                    case "1": Console.WriteLine("  ✓ Standard selected."); return ShippingType.Standard;
                    case "2": Console.WriteLine("  ✓ Express selected."); return ShippingType.Express;
                    case "3": Console.WriteLine("  ✓ Same Day selected."); return ShippingType.SameDay;
                    default: Console.WriteLine("  ✗ Invalid. Enter 1, 2, or 3."); break;
                }
            }
        }

        static PaymentMethod PromptPaymentSelection()
        {
            Console.WriteLine("  Select payment method:");
            Console.WriteLine("  [1] Cash on Delivery");
            Console.WriteLine("  [2] Credit / Debit Card");
            Console.WriteLine("  [3] E-Wallet");

            while (true)
            {
                Console.Write("\n  >> Payment Method (1/2/3): ");
                string input = Console.ReadLine()?.Trim() ?? "";

                switch (input)
                {
                    case "1": Console.WriteLine("  ✓ Cash on Delivery selected."); return PaymentMethod.CashOnDelivery;
                    case "2": Console.WriteLine("  ✓ Credit/Debit Card selected."); return PaymentMethod.CreditDebitCard;
                    case "3": Console.WriteLine("  ✓ E-Wallet selected."); return PaymentMethod.EWallet;
                    default: Console.WriteLine("  ✗ Invalid. Enter 1, 2, or 3."); break;
                }
            }
        }

        static bool PromptConfirmOrder()
        {
            Console.WriteLine();
            Console.Write("  >> Confirm and place order? (Y/N): ");
            return Console.ReadLine()?.Trim().ToUpper() == "Y";
        }

        //formattttiiingggggggggggggggggggg

        static string FormatShippingType(ShippingType type) => type switch
        {
            ShippingType.Standard => "Standard (~5 days)",
            ShippingType.Express => "Express (~2 days)",
            ShippingType.SameDay => "Same Day (~8 hours)",
            _ => type.ToString()
        };

        static string FormatPaymentMethod(PaymentMethod method) => method switch
        {
            PaymentMethod.CashOnDelivery => "Cash on Delivery (COD)",
            PaymentMethod.CreditDebitCard => "Credit / Debit Card",
            PaymentMethod.EWallet => "E-Wallet",
            _ => method.ToString()
        };

        static string FormatOrderStatus(OrderStatus status) => status switch
        {
            OrderStatus.ToPay => "To Pay — Awaiting payment confirmation",
            OrderStatus.ToShip => "To Ship — Preparing your order",
            OrderStatus.InTransit => "In Transit",
            OrderStatus.ToReceive => "To Receive",
            OrderStatus.Completed => "Completed",
            OrderStatus.Cancelled => "Cancelled",
            _ => status.ToString()
        };
    }
}
