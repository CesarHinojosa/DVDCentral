using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem
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
            Assert.AreEqual(3, dc.tblOrderItems.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblOrderItem entity = new tblOrderItem();

            entity.OrderId = 1;
            entity.Quantity = 3;
            entity.MovieId = 2;
            entity.Cost = 500;

            dc.tblOrderItems.Add(entity);
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrderItem entity = dc.tblOrderItems.FirstOrDefault();

            entity.Quantity = 6000;
          

            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //Grabs the ID
            tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblOrderItems.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
    }
}
