using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrder
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderManager.Load().Count);
        }

        //[TestMethod]
        //public void InsertTest1()
        //{
        //    int id = 0;
        //    int results = OrderManager.Insert("Inserted Description", ref id, true);

        //    Assert.AreEqual(1, results);
        //}

        //overload
        [TestMethod]
        public void InsertTest()
        {
            int id = 0;
            Order format = new Order
            {
                CustomerId = 8,
                UserId = 9
            };
            int results = OrderManager.Insert(format, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //In order to see an update you need to have things updated
            Order order = OrderManager.LoadById(3);
            order.UserId = 5;
            int results = OrderManager.Update(order, true);
            //Number of Rows updated
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}