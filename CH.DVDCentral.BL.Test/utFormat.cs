using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utFormat
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, FormatManager.Load().Count);
        }

        //[TestMethod]
        //public void InsertTest1()
        //{
        //    int id = 0;
        //    int results = FormatManager.Insert("Inserted Description", ref id, true);

        //    Assert.AreEqual(1, results);
        //}

        //overload
        [TestMethod]
        public void InsertTest()
        {
            int id = 0;
            Format format = new Format
            {
                Description = "Description"
            };
            int results = FormatManager.Insert(format, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //In order to see an update you need to have things updated
            Format format = FormatManager.LoadById(3);
            format.Description = "Updated Description";
            int results = FormatManager.Update(format, true);
            //Number of Rows updated
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = FormatManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}