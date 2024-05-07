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
            this.activeLines = activeLines;
            this.staff = LoadCashier(fileCashiers);

        }

        private Dictionary<string, Person> LoadCashier(string filename) 
        {
            Dictionary<string, Person> staff = new Dictionary<string, Person>();
            StreamReader r = new StreamReader(filename);
            string line;

            line = r.ReadLine();
            while (line != null)
            {
                string[] cashierInfo = line.Split(";");
                string[] extraccioData = cashierInfo[3].Split(" ");
                string[] dataFinal = extraccioData[0].Split("/");

                DateTime hire = new DateTime(Convert.ToInt32(dataFinal[2]), Convert.ToInt32(dataFinal[1]), Convert.ToInt32(dataFinal[0]));
                Cashier cashier = new Cashier(cashierInfo[0], cashierInfo[1], hire);
                staff.Add(cashierInfo[0], cashier);

                line = r.ReadLine();
            }
            r.Close();
            return staff;
        }
    }
}
