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
    public static class OrderItemManager
    {

        public static int Insert(int orderId,
                                 int quantity,
                                 int movieId,
                                 float cost,
                                 ref int id,       // this is by reference (ref or out)
                                 bool rollback = false) //optional paramater
        {
            try
            {
                OrderItem movie = new OrderItem
                {
                    OrderId = orderId,
                    Quantity = quantity,
                    MovieId = movieId,
                    Cost = cost
                    

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

        public static int Insert(OrderItem movie, bool rollback = false)
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
                    tblOrderItem entity = new tblOrderItem();
                    //turnary operator 
                    //s is a represenation of a student 
                    // dc.tblStudents.Any()  yes or no if it is false then you do 1;
                    //dc.tblStudents.Any()  yes or no if it is true it does this ? dc.tblStudents.Max(s => s.Id) + 1
                    entity.Id = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(m => m.Id) + 1 : 1;
                    entity.OrderId = movie.OrderId;
                    entity.Quantity = movie.Quantity;
                    entity.MovieId = movie.MovieId;
                    entity.Cost = movie.Cost;
                   


                    // IMPORTANT - BACK FILL THE ID 
                    //YOU CAN'T PASS AN OBJECT BY VALUE 
                    //IT IS BY REFERENCE 
                    movie.Id = entity.Id;

                    dc.tblOrderItems.Add(entity);
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

        public static int Update(OrderItem movie, bool rollback = false)
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
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(m => m.Id == movie.Id);

                    if (entity != null)
                    {
                        entity.OrderId = movie.OrderId;
                        entity.Quantity = movie.Quantity;
                        entity.MovieId = movie.MovieId;
                        entity.Cost = movie.Cost;
                        

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
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(m => m.Id == id);

                    if (entity != null)
                    {
                        //Removes the degreeType with the selected ID
                        dc.tblOrderItems.Remove(entity);
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

        public static OrderItem LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(m => m.Id == id);

                    if (entity != null)
                    {
                        return new OrderItem
                        {
                            Id = entity.Id,
                            OrderId = entity.OrderId,
                            Quantity = entity.Quantity,
                            MovieId = entity.MovieId,
                            Cost =(float)entity.Cost,
                            
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

        public static List<OrderItem> Load()
        {
            try
            {
                List<OrderItem> list = new List<OrderItem>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from m in dc.tblOrderItems
                     select new
                     {
                         m.Id,
                         m.OrderId,
                         m.Quantity,
                         m.MovieId,
                         m.Cost
                     })
                     .ToList()
                     .ForEach(movie => list.Add(new OrderItem
                     {
                         Id = movie.Id,
                         OrderId = movie.OrderId,
                         Quantity = movie.Quantity,
                         MovieId = movie.MovieId,
                         Cost = (float)movie.Cost,
                         

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
