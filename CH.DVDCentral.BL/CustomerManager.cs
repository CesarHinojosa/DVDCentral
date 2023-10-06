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
    public static class CustomerManager
    {

        public static int Insert(string firstname,
                                 string lastname,
                                 int userId,
                                 string address,
                                 string city,
                                 string state,
                                 string zip,
                                 string phone,
                                 string imagepath,
                                 ref int id,       // this is by reference (ref or out)
                                 bool rollback = false) //optional paramater
        {
            try
            {
                Customer customer = new Customer
                {
                    FirstName = firstname,
                    LastName = lastname,
                    UserID = userId,
                    Address = address,
                    City = city,
                    State = state,
                    Zip = zip,
                    Phone = phone,
                    ImagePath = imagepath
                    
                    

                };

                int results = Insert(customer, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID 
                id = customer.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Customer customer, bool rollback = false)
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
                    tblCustomer entity = new tblCustomer();
                    //turnary operator 
                    //s is a represenation of a student 
                    // dc.tblStudents.Any()  yes or no if it is false then you do 1;
                    //dc.tblStudents.Any()  yes or no if it is true it does this ? dc.tblStudents.Max(s => s.Id) + 1
                    entity.Id = dc.tblCustomers.Any() ? dc.tblCustomers.Max(d => d.Id) + 1 : 1;
                    entity.FirstName = customer.FirstName;
                    entity.LastName = customer.LastName;
                    entity.UserId = customer.UserID;
                    entity.Address = customer.Address;
                    entity.City = customer.City;
                    entity.State = customer.State;
                    entity.ZIP = customer.Zip;
                    entity.Phone = customer.Phone;
                    entity.ImagePath = customer.ImagePath;


                    // IMPORTANT - BACK FILL THE ID 
                    //YOU CAN'T PASS AN OBJECT BY VALUE 
                    //IT IS BY REFERENCE 
                    customer.Id = entity.Id;

                    dc.tblCustomers.Add(entity);
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

        public static int Update(Customer customer, bool rollback = false)
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
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(d => d.Id == customer.Id);

                    if (entity != null)
                    {
                        entity.FirstName = customer.FirstName;
                        entity.LastName = customer.LastName;
                        entity.UserId = customer.UserID;
                        entity.Address = customer.Address;
                        entity.City = customer.City;
                        entity.State = customer.State;
                        entity.ZIP = customer.Zip;
                        entity.Phone = customer.Phone;
                        entity.ImagePath = customer.ImagePath;
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
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(d => d.Id == id);

                    if (entity != null)
                    {
                        //Removes the student with the selected ID
                        dc.tblCustomers.Remove(entity);
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

        public static Customer LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        return new Customer
                        {
                            Id = entity.Id,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            UserID = entity.UserId,
                            Address = entity.Address,
                            City = entity.City,
                            State = entity.State,
                            Zip = entity.ZIP,
                            Phone = entity.Phone,
                            ImagePath = entity.ImagePath,

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


        public static List<Customer> Load()
        {
            try
            {
                List<Customer> list = new List<Customer>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from d in dc.tblCustomers
                     select new
                     {
                         d.Id,
                         d.FirstName,
                         d.LastName,
                         d.UserId,
                         d.Address,
                         d.City,
                         d.State,
                         d.ZIP,
                         d.Phone,
                         d.ImagePath,
                     })
                     .ToList()
                     .ForEach(customer => list.Add(new Customer
                     {
                         Id = customer.Id,
                         FirstName = customer.FirstName,
                         LastName = customer.LastName

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
