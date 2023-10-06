using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
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
            Assert.AreEqual(3, dc.tblOrders.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblOrder entity = new tblOrder();

            entity.CustomerId = 2;
            entity.OrderDate = DateTime.Now;
            entity.ShipDate = DateTime.Now;
            entity.UserId = 3;

            dc.tblOrders.Add(entity);
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrder entity = dc.tblOrders.FirstOrDefault();

            entity.CustomerId = 5;

            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //Grabs the ID
            tblOrder entity = dc.tblOrders.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblOrders.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
        
        
    }
}
