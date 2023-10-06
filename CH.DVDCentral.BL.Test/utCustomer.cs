using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utCustomer
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, CustomerManager.Load().Count);
        }

        //[TestMethod]
        //public void InsertTest1()
        //{
        //    int id = 0;
        //    int results = CustomerManager.Insert("Test Insert Customer FN", "Test Insert Customer LN", ref id, true);
        //    Assert.AreEqual(1, results);
        //}

        //overload
        [TestMethod]
        public void InsertTest()
        {
            int id = 0;
            Customer customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                UserID = 9,
                Address = "Test",
                City = "Test",  
                State = "WI", // Has to be only two characters 
                Zip = "Test",
                Phone = "Test",
                ImagePath = "Test"


            };
            int results = CustomerManager.Insert(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //In order to see an update you need to have things updated
            Customer customer = CustomerManager.LoadById(3);
            customer.FirstName = "Updated Test";
            int results = CustomerManager.Update(customer, true);
            //Number of Rows updated
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = CustomerManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}