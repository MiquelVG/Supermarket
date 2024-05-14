using System;
using System.Collections.Generic;
using System.Linq;
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

            shoppingList = new Dictionary<Item, double>();
            this.customer = customer;
            this.customer.Active = true;
            this.dateOfPurchase = dateOfPurchase;
        }

        #region PROPERTIES
        public Dictionary<Item, double> GetShoppingList { get { return shoppingList; } }

        public Customer Customer { get { return customer; } }

        public DateTime DateOfPurchase { get {  return dateOfPurchase; } }
        #endregion

        public void AddOne(Item item, double qty)
        {
            foreach(KeyValuePair<Item, double> kvp in shoppingList)
            {
                if (kvp.Key == item)
                {
                    if(kvp.Key.GetCategory == )
                }
            }
        }
    }
}
