using System.Text;

namespace Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Customer miquel = new Customer("724891D","Miquel Vidal Gomila",25);
            Console.WriteLine(miquel.FullName);
            miquel.AddInvoicedAmount(44.3);
            Console.WriteLine(miquel.GetRating);
            miquel.Active = true;
            miquel.AddPoints(34);

            Console.WriteLine(miquel);
        }
    }
}
