using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrderItem
    {
        [TestMethod]
        public void LoadTest()
        {
            //gets the orderId of 3 and makes sures there is one thing in there
            Assert.AreEqual(3, OrderItemManager.Load().Count);
        }

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
            OrderItem orderItem = OrderItemManager.LoadById(2);
            orderItem.Cost = 35;
            int result = OrderItemManager.Update(orderItem, true);
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderItemManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }

        //does not work with loadbyId
        [TestMethod]
        public void LoadByOrderIdTest()
        {
            int orderId = OrderItemManager.Load().FirstOrDefault().OrderId;
            Assert.IsTrue(OrderItemManager.Load(orderId).Count > 0);
        }
    }
}