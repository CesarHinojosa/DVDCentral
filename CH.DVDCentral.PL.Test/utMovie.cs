
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;

namespace CH.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie
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

        //Cleanup runs 
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
            Assert.AreEqual(3, dc.tblMovies.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make an entity when inserting always
            tblMovie entity = new tblMovie();

            entity.Title = "Inserted Title";
            entity.Description = "Inserted Description";
            entity.FormatId = -101;
            entity.DirectorId = -102;
            entity.RatingId = -103;
            entity.Cost = -100;
            entity.InStkQty = -99;
            entity.ImagePath = "Inserted Image";

            dc.tblMovies.Add(entity);

            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest() 
        {
            tblMovie entity = dc.tblMovies.FirstOrDefault();

            entity.Title = "Updated Title";
            entity.Description = "Updated Description";
            entity.FormatId = 50;
            entity.DirectorId = 51;
            entity.RatingId = 52;
            entity.InStkQty = 55;
            entity.Cost = 56;
            entity.ImagePath = "Updated Image Path";

            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblMovie entity = dc.tblMovies.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblMovies.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }
    }
}
