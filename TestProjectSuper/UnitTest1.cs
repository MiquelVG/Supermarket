using Supermarket;

namespace TestProjectSuper
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class UnitTestSuperMarket
        {
            [TestMethod]
            public void TestPriceArticleOnSale()
            {
                Item it = new Item(1, "a", true, 10, Category.BEVERAGE, Packaging.Unit, 10, 10);
                Assert.AreEqual(it.Price, 9.00);
            }

            [TestMethod]
            public void TestComparadorStockPerQuantitat()
            {
                var al = new List<Item>();
                al.Add(new Item(3, "a", true, 10, Category.BEVERAGE, Packaging.Unit, 30, 10));
                al.Add(new Item(1, "a", true, 10, Category.BEVERAGE, Packaging.Unit, 10, 10));
                al.Add(new Item(2, "a", true, 10, Category.BEVERAGE, Packaging.Unit, 20, 10));
                al.Sort();

                Assert.AreEqual(al[0].Stock, 10);
                Assert.AreEqual(al[1].Stock, 20);
                Assert.AreEqual(al[2].Stock, 30);
            }

            [TestMethod]
            public void TestCustomerRating()
            {
                var c = new Customer("1", "Client", null);
                c.AddInvoiceAmount(10);
                c.AddInvoiceAmount(90);
                Assert.AreEqual(100 * 0.02, c.GetRating, "El rating no és correcte");
            }

            [TestMethod]
            public void TestCustomerAddPoints()
            {
                var c = new Customer("1", "Client", null);
                c.AddPoints(10);
                Assert.AreEqual(0, c.Points, "Els punts s'haurien d'haver perdut");
                c = new Customer("1", "Client", 23);
                c.AddPoints(10);
                c.AddPoints(20);
                Assert.AreEqual(30, c.Points, "Els punts NO s'haurien d'haver perdut");
            }

            [TestMethod]
            public void TestCashierYearsOfServiceAndPoints()
            {
                var dataJoin = new System.DateTime(2001, 01, 01);
                var diff = System.DateTime.Today - dataJoin;
                Cashier c = new Cashier("1", "A", new System.DateTime(2001, 01, 01));
                Assert.AreEqual(diff.Days / 365, c.YearsOfService, "Els anys de servei no quadren...");

                c.AddInvoiceAmount(10);

                Assert.AreEqual(c.GetRating, diff.Days + 10 * 0.1, "Els punts haurien de ser anys + 10*0.1");
            }

            [TestMethod]
            public void testCarrega()
            {
                //TODO: Posar els fitxers fca.txt, fcu.txt i fi.txt a la carpeta bin on s'executen les proves unitàries.
                SuperMarket sm = new SuperMarket("super", "noimporta", "fca.txt", "fcu.txt", "fi.txt", 2);
                Assert.AreEqual(sm.warehouse.Count, 8);
                Assert.AreEqual(sm.warehouse[1].Description.Trim().ToLower(), "tropical fruit");
                Assert.AreEqual(sm.warehouse[7].Description.Trim().ToLower(), "toilet cleaner");

                Assert.AreEqual(sm.customers.Count, 6);
                Assert.AreEqual(sm.customers["CASH"].FullName.Trim().ToLower(), "generic customer");
                Assert.AreEqual(sm.customers["040302877T"].FullName.Trim().ToLower(), "francisca vallmajo trias");


                Assert.AreEqual(sm.staff.Count, 3);
                Assert.AreEqual(sm.staff["037276013M"].FullName.Trim().ToLower(), "caterina font vitales");
                Assert.AreEqual(sm.staff["040317067E"].FullName.Trim().ToLower(), "joaquim reixach negre");
            }

            [TestMethod]
            public void testCarregaCaixersAmbDatesCorrectes()
            {
                SuperMarket sm = new SuperMarket("super", "noimporta", "fca.txt", "fcu.txt", "fi.txt", 2);
                Assert.AreEqual(sm.staff.Count, 3);
                var diff = System.DateTime.Today - new System.DateTime(2015, 2, 1);
                Cashier c = (Cashier)sm.staff["037276013M"];
                Assert.AreEqual(c.FullName.Trim().ToLower(), "caterina font vitales");
                Assert.AreEqual(c.YearsOfService, diff.Days / 365);

                diff = System.DateTime.Today - new System.DateTime(2021, 11, 1);
                c = (Cashier)sm.staff["040317067E"];
                Assert.AreEqual(c.FullName.Trim().ToLower(), "joaquim reixach negre");
                Assert.AreEqual(c.YearsOfService, diff.Days / 365);
            }
        }
    }  
}