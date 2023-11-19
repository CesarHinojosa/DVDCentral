using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.BL.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [DisplayName("User ID")]
        public int UserID { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }
        [DisplayName("Image Path")]
        public string? ImagePath { get; set; }

    }
}
