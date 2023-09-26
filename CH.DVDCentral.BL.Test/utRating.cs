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
    }
}