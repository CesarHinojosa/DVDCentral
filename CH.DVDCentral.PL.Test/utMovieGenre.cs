using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovieGenre
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
            Assert.AreEqual(3, dc.tblMovieGenres.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblMovieGenre entity = new tblMovieGenre();

            entity.MovieId = 4;
            entity.GenreId = 5;

            dc.tblMovieGenres.Add(entity);
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault();

            entity.MovieId = 8;
            entity.GenreId = 9;

            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //Grabs the ID
            tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblMovieGenres.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
    }
}
