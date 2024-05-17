using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class CheckOutLine
    {
        private int number;
        private Queue<ShoppingCart> queue;
        private Person cashier;
        private bool active;

        public CheckOutLine(Person responsible, int number)
        {
            this.cashier = responsible;
            queue = new Queue<ShoppingCart>();
            this.number = number;
            this.active = true;
        }

        public int Number { get { return number; } }
        public Person Cashier { get { return cashier; } }
        public bool Active { get { return active; } }
        public bool CheckIn(ShoppingCart oneShoppingCart)
        {
            bool possible = false;
            if (Active)
            {
                queue.Enqueue(oneShoppingCart);
                possible = true;
            }
            return possible;
        }

        public bool CheckOut()
        {
            bool possible = false;
            if (Active && queue.Count != 0)
            {
                possible = true;
                ShoppingCart cart = queue.Dequeue();
                double grossAmount = ShoppingCart.ProcessItems(cart);
                int rawPoints = cart.RawPointsObtainedAtCheckout(grossAmount);
                cashier.AddInvoiceAmount(grossAmount);
                cart.Customer.AddInvoiceAmount(grossAmount);
                cashier.AddPoints(rawPoints);
                cart.Customer.AddPoints(rawPoints);
                cart.Customer.Active = false;
            }

            return possible;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"NUMERO DE CAIXA -->{number}\n");
            sb.Append($"CAIXER/A AL CÀRREC -->{cashier.FullName}\n");
            sb.Append("*********\n");
            if (queue.Count == 0) sb.Append("CUA BUIDA");
            else
            {
                foreach (ShoppingCart cart in queue)
                {
                    sb.Append($"{cart.ToString()}\n");
                }
            }

            return sb.ToString();
        }
    }
}
