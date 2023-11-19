using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.BL.Models
{
    public class Order
    {
        public int Id { get; set; }
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Ship Date")]
        public DateTime ShipDate { get; set; }
        [DisplayName("User Id")]
        public int UserId { get; set; }


    }
}
