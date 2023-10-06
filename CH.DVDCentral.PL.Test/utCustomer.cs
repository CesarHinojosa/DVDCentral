using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer
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
            Assert.AreEqual(3, dc.tblCustomers.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblCustomer entity = new tblCustomer();

            entity.FirstName = "Inserted First Name";
            entity.LastName = "Inserted Last Name";
            entity.UserId = 5;
            entity.Address = "Test Address";
            entity.City = "Appleton Test";
            entity.State = "WI";
            entity.ZIP = "54896";
            entity.Phone = "1234567890";
            entity.ImagePath = "new.jpeg";

            dc.tblCustomers.Add(entity);
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblCustomer entity = dc.tblCustomers.FirstOrDefault();

            entity.FirstName = "Updated First Name";
            entity.LastName = "Updated Last Name";
            entity.State = "CA";

            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //Grabs the ID
            tblCustomer entity = dc.tblCustomers.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblCustomers.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
    }
}
