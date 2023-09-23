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
    public static class DirectorManager
    {

        public static int Insert(string firstname,
                                 string lastname,
                                 ref int id,       // this is by reference (ref or out)
                                 bool rollback = false) //optional paramater
        {
            try
            {
                Director director = new Director
                {
                    FirstName = firstname,
                    LastName = lastname

                };

                int results = Insert(director, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID 
                id = director.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Director director, bool rollback = false)
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
                    tblDirector entity = new tblDirector();
                    //turnary operator 
                    //s is a represenation of a student 
                    // dc.tblStudents.Any()  yes or no if it is false then you do 1;
                    //dc.tblStudents.Any()  yes or no if it is true it does this ? dc.tblStudents.Max(s => s.Id) + 1
                    entity.Id = dc.tblDirectors.Any() ? dc.tblDirectors.Max(s => s.Id) + 1 : 1;
                    entity.FirstName = director.FirstName;
                    entity.LastName = director.LastName;


                    // IMPORTANT - BACK FILL THE ID 
                    //YOU CAN'T PASS AN OBJECT BY VALUE 
                    //IT IS BY REFERENCE 
                    director.Id = entity.Id;

                    dc.tblDirectors.Add(entity);
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

        public static List<Director> Load()
        {
            try
            {
                List<Director> list = new List<Director>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from d in dc.tblDirectors
                     select new
                     {
                         d.Id,
                         d.FirstName,
                         d.LastName
                     })
                     .ToList()
                     .ForEach(director => list.Add(new Director
                     {
                         Id = director.Id,
                         FirstName = director.FirstName,
                         LastName = director.LastName

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
