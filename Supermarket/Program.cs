using System.Text;

namespace Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            DateTime hire = new DateTime(2005, 09, 24);
            Cashier preslava = new Cashier("lsjbvñj", "preslava", hire);

            Console.WriteLine(preslava);
        }
    }
}
