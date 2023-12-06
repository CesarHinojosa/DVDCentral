using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CH.DVDCentral.BL.Models;
using CH.DVDCentral.PL;

namespace CH.DVDCentral.BL
{
    public static class ShoppingCartManager
    {
        public static void Add(ShoppingCart cart, Movie movie)
        {
            if (cart!= null)
            {
                cart.Items.Add(movie);
            }
        }

        public static void Remove(ShoppingCart cart, Movie movie)
        {

            if (cart != null)
            {
                cart.Items.Remove(movie);
            }
        }

        public static void CheckOut(ShoppingCart cart)
        {
            //try catches

            // make a new order
            //Set the order fields as needed

            //forearch(Movie item in cart.Items)
            //
            //Make a new orderitem 
            //Set the OrderItem fields from the item
            //order.OrderItems.Add(oderItem)

            //OrderManager.Insert(order)

            //Decrement the tblMovie.InStkQty approriately



            try
            {
                Order order = new Order();
              

                order.CustomerId = 1;
                order.UserId= 1;
                order.OrderDate = DateTime.Now;
                order.ShipDate = DateTime.Now.AddDays(3);
               

                foreach (Movie item in cart.Items)
                {
                    
                        
                    
                    OrderItem orderItem = new OrderItem();
                    order.Id = orderItem.Id;
                    orderItem.MovieId= item.Id;
                    orderItem.Quantity = 1;
                    orderItem.Cost = (float)Math.Round(item.Cost, 2);

                    Movie movie = MovieManager.LoadById(item.Id);
                    movie.InStkQty = movie.InStkQty - 1;

                    order.OrderItems.Add(orderItem);
                    OrderManager.Insert(order);
                }
            }
            catch (Exception)
            {

                throw;
            }

            cart = new ShoppingCart();

        }
    }
}
