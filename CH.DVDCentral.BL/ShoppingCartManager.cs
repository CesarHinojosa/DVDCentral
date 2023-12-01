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

            cart = new ShoppingCart();

        }
    }
}
