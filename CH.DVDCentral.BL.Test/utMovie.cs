using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovie
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, MovieManager.Load().Count);
        }

        //[TestMethod]
        //public void InsertTest1()
        //{
        //    int id = 0;
        //    int results = MovieManager.Insert("Inserted Title",
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
            Movie degreeType = new Movie
            {
                Title = "Title",
                Description = "Description",
                FormatId = 3,
                DirectorId = 2,
                RatingId = 2,
                Cost = 50.2,
                InStkQty = 25,
                ImagePath = "Inserted Image Path"
                

            };
            int results = MovieManager.Insert(degreeType, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //In order to see an update you need to have things updated
            Movie degreeType = MovieManager.LoadById(3);
            degreeType.Description = "Updated Description";
            int results = MovieManager.Update(degreeType, true);
            //Number of Rows updated
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = MovieManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}