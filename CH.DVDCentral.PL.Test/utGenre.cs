using Microsoft.EntityFrameworkCore.Storage;

namespace CH.DVDCentral.PL.Test
{
    [TestClass]
    public class utGenre
    {

        //Required in every test file 
        protected DVDCentralEntities dc;

        //Sql Transaction this holds the data
        protected IDbContextTransaction transaction;

        //Runs it
        [TestInitialize]
        public void Initialize()
        {
            //instantiate it 
            dc = new DVDCentralEntities();
            transaction = dc.Database.BeginTransaction();

        }

        //Cleanup runs/ rollbacks
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, dc.tblGenres.Count());
           
        }

        [TestMethod]
        public void InsertTest()
        {
            tblGenre entity = new tblGenre();
            entity.Description = "Inserted Description";

            dc.tblGenres.Add(entity);

            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGenre entity = dc.tblGenres.FirstOrDefault();

            entity.Description = "Updated Description for UpdateTest";

            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest() 
        {
            tblGenre entity = dc.tblGenres.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblGenres.Remove(entity);
            int result = dc.SaveChanges();  
            Assert.AreNotEqual(result, 0);

            //This would also work shows that there is something that was changed
            //Assert.AreEqual(result, 1);
        }
    }
}