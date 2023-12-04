using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.BL.Models
{
    public class Order
    {

        [DisplayName("Order #")]
        public int Id { get; set; }
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [DisplayName("Order Items")]
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Ship Date")]
        public DateTime ShipDate { get; set; }
        [DisplayName("User Id")]
        public int UserId { get; set; }


        [DisplayName("Cost")]
        public double Cost { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return Cost * Quantity; } }
       
        public double Quantity { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }
       

        [DisplayName("Customer Name")]
        public string CustomerName { get { return Lastname + ", " + Firstname; } }


    }
}
