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
                                 int quantity,
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
                    Quanity = quantity,
                    ImagePath = imagepath

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
                    entity.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(s => s.Id) + 1 : 1;
                    entity.Title = movie.Title;
                    entity.Description = movie.Description;
                    entity.FormatId = movie.FormatId;
                    entity.DirectorId = movie.DirectorId;
                    entity.RatingId = movie.RatingId;
                    entity.Cost = movie.Cost;
                    entity.Quantity = movie.Quanity;
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

        public static List<Movie> Load()
        {
            try
            {
                List<Movie> list = new List<Movie>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from m in dc.tblMovies
                     select new
                     {
                         m.Id,
                         m.Title,
                         m.Description,
                         m.FormatId,
                         m.DirectorId,
                         m.RatingId,
                         m.Cost,
                         m.Quantity,
                         m.ImagePath,
                     })
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
                         ImagePath = movie.ImagePath,

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
