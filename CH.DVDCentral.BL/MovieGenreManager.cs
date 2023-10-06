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


        public static int Insert(int movieId, int genreId, bool rollback = false)
        {

            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback)
                    {
                        transaction = dc.Database.BeginTransaction();
                    }
                    tblMovieGenre entity = new tblMovieGenre();
                    //turnary operator 
                    //s is a represenation of a student 
                    // dc.tblStudents.Any()  yes or no if it is false then you do 1;
                    //dc.tblStudents.Any()  yes or no if it is true it does this ? dc.tblStudents.Max(s => s.Id) + 1
                    entity.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(d => d.Id) + 1 : 1;
                    entity.GenreId = genreId;
                    entity.MovieId = movieId;


                    // IMPORTANT - BACK FILL THE ID 
                    //YOU CAN'T PASS AN OBJECT BY VALUE 
                    //IT IS BY REFERENCE 
                    //movieGenre.Id = entity.Id;

                    dc.tblMovieGenres.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback)
                    {
                        transaction.Rollback();
                    }

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(int movieId, int genreId, int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback)
                    {
                        transaction = dc.Database.BeginTransaction();
                    }

                    //Get the row that we are trying to Update
                    tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault(d => d.Id == id);

                    if (entity != null)
                    {
                        entity.MovieId = movieId;
                        entity.GenreId = genreId;
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback)
                    {
                        transaction.Rollback();
                    }

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback)
                    {
                        transaction = dc.Database.BeginTransaction();
                    }

                    //Gets the ID 
                    tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault(d => d.Id == id);

                    if (entity != null)
                    {
                        //Removes the student with the selected ID
                        dc.tblMovieGenres.Remove(entity);
                        results = dc.SaveChanges();

                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback)
                    {
                        transaction.Rollback();
                    }

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public static MovieGenre LoadById(int id)
        //{
        //    try
        //    {
        //        using (DVDCentralEntities dc = new DVDCentralEntities())
        //        {
        //            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault(s => s.Id == id);

        //            if (entity != null)
        //            {
        //                return new MovieGenre
        //                {
        //                    Id = entity.Id,
        //                    FirstName = entity.FirstName,
        //                    LastName = entity.LastName,

        //                };
        //            }
        //            else
        //            {
        //                throw new Exception();
        //            }
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}


        //public static List<MovieGenre> Load()
        //{
        //    try
        //    {
        //        List<MovieGenre> list = new List<MovieGenre>();

        //        using (DVDCentralEntities dc = new DVDCentralEntities())
        //        {
        //            (from d in dc.tblMovieGenres
        //             select new
        //             {
        //                 d.Id,
        //                 d.FirstName,
        //                 d.LastName
        //             })
        //             .ToList()
        //             .ForEach(movieGenre => list.Add(new MovieGenre
        //             {
        //                 Id = movieGenre.Id,
        //                 FirstName = movieGenre.FirstName,
        //                 LastName = movieGenre.LastName

        //             }));
        //        }

        //        return list;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        
    }
}
