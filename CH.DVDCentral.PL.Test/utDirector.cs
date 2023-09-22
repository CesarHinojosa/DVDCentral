using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.PL.Test
{
    [TestClass]
    public class utDirector
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
            Assert.AreEqual(3, dc.tblDirectors.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblDirector entity = new tblDirector(); 

            entity.FirstName = "Inserted First Name";
            entity.LastName = "Inserted Last Name";
            
            dc.tblDirectors.Add(entity);
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblDirector entity = dc.tblDirectors.FirstOrDefault();

            entity.FirstName = "Updated First Name";
            entity.LastName = "Updated Last Name";

            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest() 
        {
            //Grabs the ID
            tblDirector entity = dc.tblDirectors.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblDirectors.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
    }
}
