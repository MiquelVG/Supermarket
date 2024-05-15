using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    internal class ShoppingCart
    {
        private Dictionary<Item, double> shoppingList;
        private Customer customer;
        private DateTime dateOfPurchase = DateTime.Now;

        public ShoppingCart(Customer customer, DateTime dateOfPurchase)
        {
            if (customer is null) throw new ArgumentNullException("INVALID CUSTOMER");

            this.shoppingList = new Dictionary<Item, double>();
            this.customer = customer;
            this.customer.Active = true;
            this.dateOfPurchase = dateOfPurchase;
        }

        #region PROPERTIES
        public Dictionary<Item, double> GetShoppingList { get { return shoppingList; } }

        public Customer Customer { get { return customer; } }

        public DateTime DateOfPurchase { get {  return dateOfPurchase; } }
        #endregion
        /// <summary>
        /// Afegeix un ítem a la shopping cart.
        /// </summary>
        /// <param name="item">Ítem que es vol afegir.</param>
        /// <param name="qty">Quantitat d'un ítem.</param>
        /// <exception cref="Exception">Quan hi ha un nombre decimal en unitats o package.</exception>
        public void AddOne(Item item, double qty)
        {
            bool existeix = false;

            foreach(KeyValuePair<Item, double> kvp in shoppingList)
            {
                if (kvp.Key == item)
                {
                    existeix = true;
                    if(kvp.Key.PackagingType == Packaging.Unit || kvp.Key.PackagingType == Packaging.Package)
                    {
                        if (Convert.ToString(qty).Contains(".")) throw new Exception("QUANTITY CANNOT HAVE DECIMALS WHEN THE ITEM PACKAGING IS UNITS OR PACKAGES");
                        shoppingList[kvp.Key] += qty;
                    }
                    else shoppingList[kvp.Key] += qty;
                }
            }
            if (!existeix) shoppingList.Add(item, qty);
        }
        /// <summary>
        /// Afegeix ítems aleatoris a la shopping cart.
        /// </summary>
        /// <param name="warehouse">Diccionari de tot l'estoc.</param>
        public void AddAllRandomly(SortedDictionary<int, Item> warehouse) 
        {
            Random qtyItems = new Random();
            Random itemsQty = new Random();
            Random randomItem = new Random();
            int qItems = 0, iQty = 0, rItem = 0;

            qItems = qtyItems.Next(1, 11);
            
            for (int i = 0; i <= qItems; i++)
            {
                rItem = randomItem.Next(0, warehouse.Count);
                iQty = itemsQty.Next(1, 5);
                AddOne(warehouse[rItem], iQty);
            }

        }
        /// <summary>
        /// Extreu l'1% del total invoiced.
        /// </summary>
        /// <param name="totalInvoiced">Total comprat.</param>
        /// <returns>Punts</returns>
        public int RawPointsObtainedAtCheckout(double totalInvoiced) 
        {
            int points = (int)(totalInvoiced * 0.01);
            return points;
        }
        /// <summary>
        /// Calcula el preu total del shopping cart.
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <returns>Preu total.</returns>
        public static double ProcessItems(ShoppingCart cart) 
        {
            double finalPrice = 0;

            foreach (KeyValuePair<Item, double> kvp in cart.GetShoppingList) 
            {
                finalPrice += (kvp.Key.Price * kvp.Value);
                Item.UpdateStock(kvp.Key, -kvp.Value);
            }

            return Math.Round(finalPrice, 2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("*********");
            sb.Append($"INFO CARRITO DE COMPRA CLIENT -> {customer.FullName}\n");
            foreach (KeyValuePair<Item, double> kvp in shoppingList)
            {
                sb.Append($"{kvp.Key.Description}   - CAT -->{kvp.Key.GetCategory}  - QTY -->{kvp.Value}    - UNIT PRICE -->{kvp.Key.Price} {kvp.Key.Currency}");
            }
            sb.Append("*****FI CARRITO COMPRA*****");

            return sb.ToString();
        }
    }
}
