using CH.DVDCentral.BL.Models;
using CH.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.BL
{
    public static class MovieGenreManager
    {


        public static void Insert(int movieId, int genreId, bool rollback = false)
        {

            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovieGenre tblMovieGenre = new tblMovieGenre();
                    tblMovieGenre.MovieId = movieId;
                    tblMovieGenre.GenreId = genreId;
                    tblMovieGenre.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(mg => mg.Id) + 1 : 1;

                    dc.tblMovieGenres.Add(tblMovieGenre);
                    dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //works but only for one genre
        //public static void Update(int movieId, int genreId, bool rollback = false)
        //{

        //    try
        //    {
        //        int results = 0;

        //        using (DVDCentralEntities dc = new DVDCentralEntities())
        //        {
        //            IDbContextTransaction transaction = null;
        //            if (rollback)
        //            {
        //                transaction = dc.Database.BeginTransaction();
        //            }

        //            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault(s => s.Id == movieId);
        //            if(entity != null)
        //            {
        //                entity.MovieId = movieId;
        //                entity.GenreId= genreId;
        //                results = dc.SaveChanges();
        //            }
        //            else
        //            {
        //                throw new Exception("Row does not exist");
        //            }

        //            if (rollback)
        //            {
        //                transaction.Rollback();
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        public static void Delete(int movieid, int genreid, bool rollback = false)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovieGenre? tblMovieGenre = dc.tblMovieGenres
                                                    .FirstOrDefault(mg => mg.MovieId == movieid
                                                    && mg.GenreId == genreid);

                    if (tblMovieGenre != null)
                    {
                        dc.tblMovieGenres.Remove(tblMovieGenre);
                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        
    }
}
