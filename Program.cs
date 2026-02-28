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

namespace ECommerceITPProject
{

    // ONGOING ADDRESS SELECTION!!!
    public class Address
    {
        public string Label { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
    }
    internal class Program
    {
        //public static string[] PaymentMethods = { "Cash on Delivery", "Credit/Debit Card", "E-Wallet" };
        //public static string[] ShippingOptions = { "Door to Door Delivery", "Pick Up Point", "Standard", "Express", "Same Day" };
        public static List<string> OrderItems = new List<string>();
        public static List<Address> PresetAddreseses = new List<Address> // PRESETS ONLY!
        {
            new Address
            {
                Label = "Default",
                FullName = "Juan Dela Cruz",
                PhoneNumber = "09061234567",
                StreetName = "123 Main St",
                City = "Makati",
                Province = "Metro Manila",
                Region = "NCR",
                PostalCode = "1234"
            }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("============== ECOMMERCE SYSTEM | ORDER MANAGEMENT PAGE: BUYER'S END ==============");
            Console.WriteLine("\tversion: 0.2 | fixed order calculation & ongoing address mgt\n");

            Console.WriteLine("----- Sample Items -----");
            ListedItems();

            Console.WriteLine("\n----- Select Items to Order -----");
            AddItems(OrderItems);

            Console.WriteLine("\n------ Order Details -----");
            ShowOrderDetails(OrderItems);
        }

        public static void ListedItems()
        {
            Console.WriteLine("1. Item A - PHP 10.00");
            Console.WriteLine("2. Item B - PHP 15.00");
            Console.WriteLine("3. Item C - PHP 20.00");
            Console.WriteLine("4. Item D - PHP 25.00");
        }

        public static void AddItems(List<string> orderItems)
        {
            List<string> ItemsToOrder = new List<string> { "A", "B", "C", "D" };

            Console.WriteLine("Enter the item you want to order (A, B, C, D).\n" +
                    "Add the same item to correspond multiple quantity.\n" +
                    "Type 'done' to finish.\n");

            while (true)
            {
                Console.Write(">> Item: ");
                string input = Console.ReadLine().ToUpper();

                if (input == "DONE")
                {
                    break;
                }

                if (ItemsToOrder.Contains(input))
                {
                    orderItems.Add(input);
                    Console.WriteLine($"You have selected item {input}.\n");
                }
                else
                {
                    Console.WriteLine("Invalid item. Please try again.\n");
                }
            }

            Console.WriteLine("\nYou have finished selecting items.");
        }

        static void ShowOrderDetails(List<string> OrderItems)
        {
            // dictionary = class type sa System.Collections.Generic namespace
            // I used string instead of char kasi for future use, kapag ang item name ay naging IRL listing
            Dictionary<string, double> ItemPrices = new Dictionary<string, double>()
            {
                { "A", 10.00 },
                { "B", 15.00 },
                { "C", 20.00 },
                { "D", 25.00 }
            };

            double RawMerchSubtotal = 0.00; // total prices ng items bago mga discounts, vouchers

            if (OrderItems.Count == 0)
            {
                Console.WriteLine("No items in order.");
                return;
            }

            Dictionary<string, int> ItemQuantity = new Dictionary<string, int>();
            {
                foreach (string item in OrderItems)
                {
                    if (ItemQuantity.ContainsKey(item))
                    {
                        ItemQuantity[item]++;
                    }
                    else
                    {
                        ItemQuantity[item] = 1;
                    }
                }

            }

            foreach (var item in ItemQuantity)
            {
                string ItemName = item.Key;
                int quantity = item.Value;
                double price = ItemPrices[ItemName];
                double TotalItemAmount = price * quantity;

                Console.WriteLine($"[{ItemName}] {quantity} x PHP {price} each = PHP {TotalItemAmount}");
                RawMerchSubtotal += TotalItemAmount;
            }

            Console.WriteLine($"\n\t>>> TOTAL RAW MERCHANDISE TOTAL: PHP {RawMerchSubtotal}");
        }

    }
}
