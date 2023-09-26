using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utDirector
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, DirectorManager.Load().Count);
        }

        //[TestMethod]
        //public void InsertTest1()
        //{
        //    int id = 0;
        //    int results = DirectorManager.Insert("Test Insert Director FN", "Test Insert Director LN", ref id, true);
        //    Assert.AreEqual(1, results);
        //}

        //overload
        [TestMethod]
        public void InsertTest()
        {
            int id = 0;
            Director student = new Director
            {
                FirstName = "Test",
                LastName = "Test"
                
            };
            int results = DirectorManager.Insert(student, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //In order to see an update you need to have things updated
            Director director = DirectorManager.LoadById(3);
            director.FirstName = "Updated Test";
            int results = DirectorManager.Update(director, true);
            //Number of Rows updated
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = DirectorManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}