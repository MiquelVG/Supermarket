using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public class SuperMarket
    {
        public static int MAXLINES = 5;
        private string name;
        private string address;
        private int activeLines;
        // private CheckOutLine[] lines = new CheckOutLine[MAXLINES];
        private Dictionary<string,Person> staff;
        private Dictionary<string, Person> customers;
        private SortedDictionary<int, Item> warehouse;

        public SuperMarket(string name, string address, string fileCashiers, string fileCustomers, string fileItems, int activeLines)
        {
            if (name == "" || name is null) throw new ArgumentNullException("INVALID NAME");
            if (address == "" || address is null) throw new ArgumentNullException("INVALID ADDRESS");
            if (fileCashiers == "" || fileCashiers is null) throw new ArgumentNullException("INVALID CASHIERS FILE");
            if (fileCustomers == "" || fileCustomers is null) throw new ArgumentNullException("INVALID CUSTOMERS FILE");
            if (fileItems == "" || fileItems is null) throw new ArgumentNullException("INVALID ITEMS FILE");
            if (activeLines < 1 || activeLines > 5) throw new IndexOutOfRangeException("INVALID ACTIVE LINES");

            this.name = name;
            this.address = address;
            this.staff
        }
    }
}
