using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utGenre
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, GenreManager.Load().Count);
        }

        //[TestMethod]
        //public void InsertTest1()
        //{
        //    int id = 0;
        //    int results = GenreManager.Insert("Inserted Description", ref id, true);

        //    Assert.AreEqual(1, results);
        //}

        //overload
        [TestMethod]
        public void InsertTest()
        {
            int id = 0;
            Genre degreeType = new Genre
            {
                Description = "Description"
            };
            int results = GenreManager.Insert(degreeType, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //In order to see an update you need to have things updated
            Genre degreeType = GenreManager.LoadById(3);
            degreeType.Description = "Updated Description";
            int results = GenreManager.Update(degreeType, true);
            //Number of Rows updated
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = GenreManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}