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
    public static class GenreManager
    {

        public static int Insert(string description,
                                ref int id,       // this is by reference (ref or out)
                                bool rollback = false) //optional paramater
        {
            try
            {
                Genre genre = new Genre
                {
                    Description = description

                };

                int results = Insert(genre, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID 
                id = genre.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Genre genre, bool rollback = false)
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
                    tblGenre entity = new tblGenre();
                    //turnary operator 
                    //s is a represenation of a student 
                    // dc.tblStudents.Any()  yes or no if it is false then you do 1;
                    //dc.tblStudents.Any()  yes or no if it is true it does this ? dc.tblStudents.Max(s => s.Id) + 1
                    entity.Id = dc.tblGenres.Any() ? dc.tblGenres.Max(s => s.Id) + 1 : 1;
                    entity.Description = genre.Description;


                    // IMPORTANT - BACK FILL THE ID 
                    //YOU CAN'T PASS AN OBJECT BY VALUE 
                    //IT IS BY REFERENCE 
                    genre.Id = entity.Id;

                    dc.tblGenres.Add(entity);
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

        public static List<Genre> Load()
        {
            try
            {
                List<Genre> list = new List<Genre>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from g in dc.tblGenres
                     select new
                     {
                         g.Id,
                         g.Description
                     })
                     .ToList()
                     .ForEach(genre => list.Add(new Genre
                     {
                         Id = genre.Id,
                         Description = genre.Description,
                         
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
