using System.Text;

namespace Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("--------- TEST ITEM ---------");
            Item patata = new Item(98, "patata", true, 24.9, Category.VEGETABLES, Packaging.Kg, 200, 50);
            Console.WriteLine(patata);
            Console.WriteLine(patata.Description);
            Console.WriteLine(patata.GetCategory);
            Console.WriteLine(patata.MinStock);
            Console.WriteLine(patata.OnSale);
            Console.WriteLine(patata.PackagingType);
            Console.WriteLine(patata.Price);
            Console.WriteLine(patata.Stock);
            Item.UpdateStock(patata, -100);
            Console.WriteLine(patata);
            Item.UpdateStock(patata, -50);
            Console.WriteLine(patata);
            Item.UpdateStock(patata, 10);
            Console.WriteLine(patata);

            Console.WriteLine();
            Console.WriteLine("--------- TEST CASHIER ---------");
            DateTime hired = new DateTime(2005, 09, 24);
            Cashier preslava = new Cashier("724891D", "Preslava Radoslavova Aleksandrova", hired);
            Console.WriteLine(preslava.FullName);
            Console.WriteLine(preslava.YearsOfService);
            preslava.AddInvoicedAmount(214.13);
            preslava.Active = true;
            preslava.AddPoints(40);
            Console.WriteLine(preslava);
            Console.WriteLine(preslava.GetRating);

            Console.WriteLine();
            Console.WriteLine("--------- TEST CUSTOMER ---------");
            Customer miquel = new Customer("724891D", "Miquel Vidal Gomila", 25);
            Console.WriteLine(miquel.FullName);
            miquel.AddInvoicedAmount(44.3);
            Console.WriteLine(miquel.GetRating);
            miquel.Active = true;
            miquel.AddPoints(34);
            Console.WriteLine(miquel);

            Console.WriteLine();
            Console.WriteLine("--------- LOAD FILES ---------");
            SuperMarket super = new SuperMarket("Mercadona","C/Carrer Eixample n21","CASHIERS.TXT","CUSTOMERS.TXT","GROCERIES.TXT",3);
            Console.Write(super.ToString());
        }
    }
}
