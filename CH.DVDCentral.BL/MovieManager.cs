using CH.DVDCentral.BL.Models;
using CH.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.BL
{
    public static class MovieManager
    {

        public static int Insert(string title,
                                 string description,
                                 int formatId,
                                 int directorId,
                                 int ratingId,
                                 float cost,
                                 int InStkQty,
                                 string imagepath,
                                 ref int id,       // this is by reference (ref or out)
                                 bool rollback = false) //optional paramater
        {
            try
            {
                Movie movie = new Movie
                {
                    Title = title,
                    Description = description,
                    FormatId = formatId,
                    DirectorId = directorId,
                    RatingId = ratingId,
                    Cost = cost,
                    InStkQty = InStkQty,
                    ImagePath = imagepath,
                    

                };

                int results = Insert(movie, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID 
                id = movie.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Movie movie, bool rollback = false)
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
                    tblMovie entity = new tblMovie();
                    //turnary operator 
                    //s is a represenation of a student 
                    // dc.tblStudents.Any()  yes or no if it is false then you do 1;
                    //dc.tblStudents.Any()  yes or no if it is true it does this ? dc.tblStudents.Max(s => s.Id) + 1
                    entity.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(m => m.Id) + 1 : 1;
                    entity.Title = movie.Title;
                    entity.Description = movie.Description;
                    entity.FormatId = movie.FormatId;
                    entity.DirectorId = movie.DirectorId;
                    entity.RatingId = movie.RatingId;
                    entity.Cost = movie.Cost;
                    entity.InStkQty = movie.InStkQty;
                    entity.ImagePath = movie.ImagePath;


                    // IMPORTANT - BACK FILL THE ID 
                    //YOU CAN'T PASS AN OBJECT BY VALUE 
                    //IT IS BY REFERENCE 
                    movie.Id = entity.Id;

                    dc.tblMovies.Add(entity);
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

        public static int Update(Movie movie, bool rollback = false)
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
                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == movie.Id);

                    if (entity != null)
                    {
                        entity.Title = movie.Title;
                        entity.Description = movie.Description;
                        entity.FormatId = movie.FormatId;
                        entity.DirectorId = movie.DirectorId;
                        entity.RatingId = movie.RatingId;
                        entity.Cost = movie.Cost;
                        entity.InStkQty = movie.InStkQty;
                        entity.ImagePath = movie.ImagePath;
                       
                        
                        
                       

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
                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == id);

                    if (entity != null)
                    {
                        //Removes the degreeType with the selected ID
                        dc.tblMovies.Remove(entity);
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

        public static Movie LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == id);

                    if (entity != null)
                    {
                        return new Movie
                        {
                            Id = entity.Id,
                            Title = entity.Title,
                            Description = entity.Description,
                            FormatId = entity.FormatId,
                            DirectorId = entity.DirectorId,
                            RatingId = entity.RatingId,
                            Cost = entity.Cost,
                            InStkQty = entity.InStkQty,
                            ImagePath = entity.ImagePath,
                            Genres = GenreManager.Load(id)
                        };
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public static List<Movie> Load(int? genreId = null)
        {
            try
            {
                List<Movie> list = new List<Movie>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    
                    (from m in dc.tblMovies
                     join d in dc.tblDirectors on m.DirectorId equals d.Id
                     join r in dc.tblRatings on m.RatingId equals r.Id
                     join f in dc.tblFormats on m.FormatId equals f.Id
                     join mg in dc.tblMovieGenres on m.Id equals mg.MovieId 
                     where mg.GenreId == genreId || genreId == null
                     
                     select new
                     {
                         m.Id,
                         m.Title,
                         m.Description,
                         m.FormatId,
                         m.DirectorId,
                         m.RatingId,
                         m.Cost,
                         m.InStkQty,
                         m.ImagePath,
                         RatingDescription = r.Description,
                         FormatDescription = f.Description,
                         //GenreDescription = mg.
                         FullName = d.FirstName + " " + d.LastName,

                     })
                     .Distinct()
                     .ToList()
                     .ForEach(movie => list.Add(new Movie
                     {
                         Id = movie.Id,
                         Title = movie.Title,
                         Description = movie.Description,
                         FormatId = movie.FormatId,
                         DirectorId = movie.DirectorId,
                         RatingId = movie.RatingId,
                         Cost = movie.Cost,
                         InStkQty = movie.InStkQty,
                         ImagePath = movie.ImagePath,
                         RatingDescription = movie.RatingDescription,
                         FormatDescription = movie.FormatDescription,
                         FullName = movie.FullName

                     }));
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
