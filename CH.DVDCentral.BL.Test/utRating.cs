using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utRating
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, RatingManager.Load().Count);
        }

        //[TestMethod]
        //public void InsertTest1()
        //{
        //    int id = 0;
        //    int results = RatingManager.Insert("Inserted Description", ref id, true);

        //    Assert.AreEqual(1, results);
        //}

        //overload
        [TestMethod]
        public void InsertTest()
        {
            int id = 0;
            Rating degreeType = new Rating
            {
                Description = "Description"
            };
            int results = RatingManager.Insert(degreeType, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //In order to see an update you need to have things updated
            Rating rating = RatingManager.LoadById(3);
            rating.Description = "Updated Description";
            int results = RatingManager.Update(rating, true);
            //Number of Rows updated
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = RatingManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }



        //[TestMethod]
        //public void InsertOrderItemsTest()
        //{
        //    Order order = new Order();
        //    {
        //        CustomerId = 99;
        //        OrderDate = DateTime.Now;
        //        UserId = 99;
        //        ShipDate = DateTime.Now;
        //        OrderItem item= new List<OrderItem>();
        //        {
        //            new OrderItem
        //            {
        //                Id = 88,
        //                MovieId = 1,
        //                Cost = 11,
        //                Quantity = 65

        //            },
        //            new OrderItem
        //            {
        //                Id = 99,
        //                MovieId = 2,
        //                Cost = 19,
        //                Quantity = 2
        //            }
        //        }


        //    };

        //    int result = OrderManager.Insert(order, true);
        //    Assert.AreEqual(order.OrderItems[1].OrderId, order.Id);
        //    Assert.AreEqual(3, resutlt);

        //[TestMethod]
        //public void LoadByIdTest()
        //{
        //    int id = OrderManager.Load().LastOrDefault().Id;
        //    Order order = OrderManager.LoadById(id);
        //    Assert.AreEqual(order.Id, id);
        //    Assert.IsTure(order.OrderItems.Count > 0);
        //}

        //[TestMethod]
        //public void LoadByIdCustomerIdTest()
        //{
        //    int cusomterId = OrderManager.Load().FirstOrDefault().Id.CustomerId;
        //    Assert.AreEqual(OrderManager.LoadById(customerId).CustomerId, customerId);
        //}


        //[TestMethod]
        //public void LoadByOrderIdTest()
        //{
        //    int orderId = OrderItemManager.Load().FirstOrDefault().OrderId;
        //    Assert.IsTure(OrderItemManager.LoadById(orderId).Count > 0);
        //}
    }
}