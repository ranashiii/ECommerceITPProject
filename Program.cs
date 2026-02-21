/* PROJECT IN ITP
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

using System;
using System.Collections.Generic;

namespace ECommerceITPProject
{
    internal class Program
    {
        //public static string[] PaymentMethods = { "Cash on Delivery", "Credit/Debit Card", "E-Wallet" };
        //public static string[] ShippingOptions = { "Door to Door Delivery", "Pick Up Point", "Standard", "Express", "Same Day" };
        //public static string[] SampleAddresses = { "Default", "Work", "Home" }; // TO BE CHANGED, SAMPLE ONLY
        public static List<string> OrderItems = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("============== ECOMMERCE SYSTEM | ORDER MANAGEMENT PAGE: BUYER'S END ==============");
            Console.WriteLine("\t\t\tversion:0.1 | layout & plotting\n");

            Console.WriteLine("----- Sample Items -----");
            ListedItems();

            Console.WriteLine("\n----- Select Items to Order -----");
            AddItems(OrderItems);

            Console.WriteLine("\n------ Order Details -----");
            ShowOrderDetails();
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
            List<string> itemsToOrder = new List<string> { "A", "B", "C", "D" };

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

                if (itemsToOrder.Contains(input))
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

        static void ShowOrderDetails()
        {
            //kulang pa, code still tentative. add quantity and price per item, etc.

            if (OrderItems.Count == 0)
            {
                Console.WriteLine("No items in order.");
            }
            else
            {
                int count = 1;
                foreach (string item in OrderItems)
                {
                    Console.WriteLine($"{count}. {item}");
                    count++;
                }
                Console.WriteLine($"Total items: {OrderItems.Count}");
            }
        }
    }
}
