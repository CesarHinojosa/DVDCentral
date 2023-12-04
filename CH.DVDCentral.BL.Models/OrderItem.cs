using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.BL.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [DisplayName("Order Id")]
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        [DisplayName("Movie Id")]
        public int MovieId { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Cost { get; set; }

        [DisplayName("Movie Title")]
        public string MovieTitle { get; set; }

        [DisplayName("Movie Image")]
        public string ImagePath { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get { return LastName + ", " + FirstName; } }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }


        [DisplayName("Sub Total")]
        public double SubTotal { get; set; }

        public double Tax { get { return SubTotal * 0.055; } }

        public double Total { get { return SubTotal + Tax; } }












    }
}
