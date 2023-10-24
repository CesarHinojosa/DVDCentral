using CH.DVDCentral.BL.Models;
using CH.DVDCentral.PL;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.BL
{
    //static classes cannot have any properties
    public static class OrderManager
    {
        //This is by value 
        public static int Insert(int customerId,
                                 int userId,
                                 ref int id,       // this is by reference (ref or out)
                                 bool rollback = false) //optional paramater
        {
            //What do you need in order for this method to run 
            //Reference or by value 
            //No idea what the ID is
            //We know the FN,LN,OrderID
            
            try
            {
                Order order = new Order
                {
                    CustomerId = customerId,
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    ShipDate = DateTime.Now,
                    
 
                };

                //this should be the one that changes
                int results = Insert(order, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID 
                id = order.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

       

        //this does all the hard lifting this is the overlaod method 
        public static int Insert(Order order, bool rollback = false)
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
                    tblOrder entity = new tblOrder();
                    //turnary operator 
                    //s is a represenation of a order 
                    // dc.tblOrders.Any()  yes or no if it is false then you do 1;
                    //dc.tblOrders.Any()  yes or no if it is true it does this ? dc.tblOrders.Max(s => s.Id) + 1
                    entity.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(s => s.Id) + 1 : 1;
                    entity.CustomerId = order.CustomerId;
                    entity.UserId = order.UserId;
                    entity.OrderDate = DateTime.Now;
                    entity.ShipDate = DateTime.Now;

                    foreach (OrderItem item in order.OrderItems)
                    {
                       
                        item.OrderId = entity.Id;
                        results += OrderItemManager.Insert(item, rollback);
                    }


                    // IMPORTANT - BACK FILL THE ID 
                    //YOU CAN'T PASS AN OBJECT BY VALUE 
                    //IT IS BY REFERENCE 
                    order.Id = entity.Id;
                    
                    

                    dc.tblOrders.Add(entity);
                    results += dc.SaveChanges();

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

        public static int Update(Order order, bool rollback = false)
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
                    tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == order.Id);

                    if (entity != null)
                    {
                        entity.CustomerId = order.CustomerId;
                        entity.UserId = order.UserId;
                        entity.ShipDate = DateTime.Now;
                        entity.OrderDate = DateTime.Now;
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
                    tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        //Removes the order with the selected ID
                        dc.tblOrders.Remove(entity);
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

        public static Order LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        return new Order
                        {
                            Id = entity.Id,
                            CustomerId = entity.CustomerId,
                            UserId = entity.UserId,
                            ShipDate = entity.ShipDate,
                            OrderDate = entity.OrderDate,
                            OrderItems = OrderItemManager.Load(entity.Id)
                          
                            
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

        public static List<Order> Load(int? customerId = null)
        {
            try
            {
                List<Order> list = new List<Order>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from d in dc.tblOrders
                     where d.CustomerId == customerId || customerId == null
                     select new
                     {
                         d.Id,
                         d.CustomerId,
                         d.UserId,
                         d.OrderDate,
                         d.ShipDate,
                         

                     })
                     .ToList()
                     .ForEach(order => list.Add(new Order
                     {
                         Id = order.Id,
                         CustomerId = order.CustomerId,
                         UserId = order.UserId,
                         OrderDate = order.OrderDate,
                         ShipDate = order.ShipDate,
                         OrderItems = OrderItemManager.Load(order.Id)
                         


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
