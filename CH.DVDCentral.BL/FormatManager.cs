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
    public static class FormatManager
    {

        public static int Insert(string description,
                                ref int id,       // this is by reference (ref or out)
                                bool rollback = false) //optional paramater
        {
            try
            {
                Format format = new Format
                {
                    Description = description

                };

                int results = Insert(format, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID 
                id = format.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Format format, bool rollback = false)
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
                    tblFormat entity = new tblFormat();
                    //turnary operator 
                    //s is a represenation of a student 
                    // dc.tblStudents.Any()  yes or no if it is false then you do 1;
                    //dc.tblStudents.Any()  yes or no if it is true it does this ? dc.tblStudents.Max(s => s.Id) + 1
                    entity.Id = dc.tblFormats.Any() ? dc.tblFormats.Max(f => f.Id) + 1 : 1;
                    entity.Description = format.Description;


                    // IMPORTANT - BACK FILL THE ID 
                    //YOU CAN'T PASS AN OBJECT BY VALUE 
                    //IT IS BY REFERENCE 
                    format.Id = entity.Id;

                    dc.tblFormats.Add(entity);
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

        public static int Update(Format format, bool rollback = false)
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
                    tblFormat entity = dc.tblFormats.FirstOrDefault(f => f.Id == format.Id);

                    if (entity != null)
                    {
                        entity.Description = format.Description;

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
                    tblFormat entity = dc.tblFormats.FirstOrDefault(f => f.Id == id);

                    if (entity != null)
                    {
                        //Removes the format with the selected ID
                        dc.tblFormats.Remove(entity);
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

        public static Format LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblFormat entity = dc.tblFormats.FirstOrDefault(f => f.Id == id);

                    if (entity != null)
                    {
                        return new Format
                        {
                            Id = entity.Id,
                            Description = entity.Description
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

            public static List<Format> Load()
            {
                try
                {
                    List<Format> list = new List<Format>();

                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        (from f in dc.tblFormats
                         select new
                         {
                             f.Id,
                             f.Description
                         })
                         .ToList()
                         .ForEach(format => list.Add(new Format
                         {
                             Id = format.Id,
                             Description = format.Description,

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
