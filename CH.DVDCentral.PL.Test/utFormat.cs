using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.PL.Test
{
    [TestClass]
    public class utFormat
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
            Assert.AreEqual(3, dc.tblFormats.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblFormat entity = new tblFormat();

            entity.Description = "Inserted Rating Description";

            dc.tblFormats.Add(entity);

            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblFormat entity = dc.tblFormats.FirstOrDefault();

            entity.Description = "Updated Rating Description";

            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblFormat entity = dc.tblFormats.Where(e => e.Id == 1).FirstOrDefault();

            dc.tblFormats.Remove(entity);

            int result = dc.SaveChanges();

            Assert.AreNotEqual(result, 0);
        }
    }
}
