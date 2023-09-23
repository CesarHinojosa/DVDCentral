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
    public static class RatingManager
    {

        public static int Insert(string description,
                                ref int id,       // this is by reference (ref or out)
                                bool rollback = false) //optional paramater
        {
            try
            {
                Rating rating = new Rating
                {
                    Description = description

                };

                int results = Insert(rating, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID 
                id = rating.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Rating rating, bool rollback = false)
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
                    tblRating entity = new tblRating();
                    //turnary operator 
                    //s is a represenation of a student 
                    // dc.tblStudents.Any()  yes or no if it is false then you do 1;
                    //dc.tblStudents.Any()  yes or no if it is true it does this ? dc.tblStudents.Max(s => s.Id) + 1
                    entity.Id = dc.tblRatings.Any() ? dc.tblRatings.Max(s => s.Id) + 1 : 1;
                    entity.Description = rating.Description;


                    // IMPORTANT - BACK FILL THE ID 
                    //YOU CAN'T PASS AN OBJECT BY VALUE 
                    //IT IS BY REFERENCE 
                    rating.Id = entity.Id;

                    dc.tblRatings.Add(entity);
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

        public static List<Rating> Load()
        {
            try
            {
                List<Rating> list = new List<Rating>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from r in dc.tblRatings
                     select new
                     {
                         r.Id,
                         r.Description
                     })
                     .ToList()
                     .ForEach(rating => list.Add(new Rating
                     {
                         Id = rating.Id,
                         Description = rating.Description,

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
