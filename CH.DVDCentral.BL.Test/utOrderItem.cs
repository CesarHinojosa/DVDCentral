using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrderItem
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderItemManager.Load().Count);
        }

        //[TestMethod]
        //public void InsertTest1()
        //{
        //    int id = 0;
        //    int results = OrderItemManager.Insert("Inserted Title",
        //                                       "Inserted Description",
        //                                       1,
        //                                       3,
        //                                       2,
        //                                       400,
        //                                       50,
        //                                       "new Image",
        //                                       ref id,
        //                                       true);

        //    Assert.AreEqual(1, results);
        //}

        //overload
        [TestMethod]
        public void InsertTest()
        {
            int id = 0;
            OrderItem orderItem = new OrderItem
            {
                MovieId = 4,
                Quantity = 500,
                OrderId = 3,
                Cost = 50
                


            };
            int results = OrderItemManager.Insert(orderItem, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //In order to see an update you need to have things updated
            OrderItem orderItem = OrderItemManager.LoadById(3);
            orderItem.Cost = 35;
            int results = OrderItemManager.Update(orderItem, true);
            //Number of Rows updated
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderItemManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}