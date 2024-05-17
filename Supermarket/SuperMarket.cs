using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        private CheckOutLine[] lines = new CheckOutLine[MAXLINES];
        private Dictionary<string, Person> staff;
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
            this.customers = LoadCustomers(fileCustomers);
            this.warehouse = LoadWarehous(fileItems);
        }

        #region LOAD DICTIONARIES FROM FILES
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

        private Dictionary<string, Person> LoadCustomers(string filename)
        {
            Dictionary<string, Person> customers = new Dictionary<string, Person>();
            Customer customer;
            StreamReader r = new StreamReader(filename);
            string line;

            line = r.ReadLine();

            while (line != null)
            {
                string[] infoCustomer = line.Split(";");
                if (infoCustomer[0] is not "CASH")
                {
                    if (infoCustomer[2] is not "") customer = new Customer(infoCustomer[0], infoCustomer[1], Convert.ToInt32(infoCustomer[2]));
                    else customer = new Customer(infoCustomer[0], infoCustomer[1], null);

                    customers.Add(infoCustomer[0], customer);
                }

                line = r.ReadLine();
            }
            r.Close();

            return customers;
        }

        private SortedDictionary<int, Item> LoadWarehous(string fileName)
        {
            SortedDictionary<int, Item> warehouse = new SortedDictionary<int, Item>();
            Item stock;
            StreamReader r = new StreamReader(fileName);
            string line;
            int i = 0;
            line = r.ReadLine();
            while (line != null)
            {
                string[] item = line.Split(";");
                Category category = (Category)Convert.ToInt32(item[1]);
                Packaging pack;
                if (item[2] == "K") pack = Packaging.Kg;
                else if (item[2] == "U") pack = Packaging.Unit;
                else pack = Packaging.Package;
                stock = new Item(i, item[0], false, Convert.ToDouble(item[3]), category, pack, i + 5, i);
                warehouse.Add(i, stock);
                i++;
                line = r.ReadLine();
            }
            r.Close();
            return warehouse;
        }
        #endregion

        #region METHODS SUPERMARKET
        public HashSet<Item> GetItemsByStock()
        {
            HashSet<Item> stock = new HashSet<Item>();
            foreach (KeyValuePair<int, Item> item in warehouse)
            {
                stock.Add(item.Value);
            }
            return stock;
        }
        #endregion

        #region METHODS TO GET CUSTOMER AND CASHIER
        public Person GetAvailableCustomer()
        {
            Person[] valorCustomer = customers.Values.ToArray();
            Person availableCustomer = null;
            int i = 0;
            while (i <= valorCustomer.Length && availableCustomer == null)
            {
                if (!valorCustomer[i].Active)
                {

                    availableCustomer = valorCustomer[i];
                    valorCustomer[i].Active = true;
                }
                else i++;
            }
            return availableCustomer;
        }
        public Person GetAvailableCashier()
        {
            Person[] valorStaff = staff.Values.ToArray();
            Person availableStaff = null;
            int i = 0;
            while (i <= valorStaff.Length && availableStaff == null)
            {
                if (!valorStaff[i].Active)
                {
                    availableStaff = valorStaff[i];
                    valorStaff[i].Active = true;
                }
                else i++;
            }

            return availableStaff;
        }
        #endregion

        #region PROPERTIES

        public SortedDictionary<int, Item> Warehouse { get { return warehouse; } }

        public Dictionary<string, Person> Staff { get { return staff; } }

        public Dictionary<string, Person> Customers { get { return customers; } }

        public int ActiveLines { get { return activeLines; } }

        #endregion

        public void OpenCheckOutLine(int line2Open)
        {
            if (line2Open < 1 || line2Open > 5) throw new IndexOutOfRangeException("INCORRECT LINE NUMBER");
            CheckOutLine line = null;
            bool trobat = false;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Number == line2Open) trobat = true;
            }
            if (!trobat)
            {
                lines[line2Open - 1] = new CheckOutLine(GetAvailableCashier(), line2Open);
            }
            else throw new Exception("THIS LINES ALREADY EXISTS");
        }

        public CheckOutLine GetCheckOutLine(int lineNumber) 
        {
            if (lineNumber < 1 || lineNumber > 5) throw new IndexOutOfRangeException("INCORRECT LINE NUMBER");
            if (lineNumber is default(int)) throw new ArgumentNullException("INCORRECT LINE NUMBER");
            int i = 0;
            bool trobat = false;
            CheckOutLine line = null;

            while (i < lines.Length && !trobat)
            {
                if (lines[i].Number == lineNumber)
                {
                    trobat = true;
                    line = lines[i];
                }
                    
                else i++;
            }

            return line;
        }

        public bool JoinTheQueue(ShoppingCart theCart, int line)
        {
            bool posible = false;
            CheckOutLine cua;

            cua = GetCheckOutLine(line);
            if (cua is not null) posible = cua.CheckIn(theCart);

            return posible;
        }

        public bool CheckOut(int line)
        {
            bool posible = false;
            CheckOutLine cua;

            cua = GetCheckOutLine(line);
            if (cua is not null) posible = cua.CheckOut();

            return posible;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.name}    {this.address}  {this.activeLines}\n");
            sb.Append("STAFF INFORMATION ---------------------------------------------\n");
            foreach (KeyValuePair<string, Person> cashier in staff)
            {
                sb.Append($"{cashier.Value}\n");
            }
            sb.Append("COSTUMERS INFORMATION ---------------------------------------------\n");
            foreach (KeyValuePair<string, Person> costumer in customers)
            {
                sb.Append($"{costumer.Value}\n");
            }
            sb.Append("STOCK INFORMATION ---------------------------------------------\n");
            foreach (KeyValuePair<int, Item>item in warehouse)
            {
                sb.Append($"{item.Value}\n");
            }
            return sb.ToString();
        }
    }
}
