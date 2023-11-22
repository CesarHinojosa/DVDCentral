using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.BL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [DisplayName("Type of Format")]
        public int FormatId { get; set; }
        [DisplayName("Director ")]
        public int DirectorId { get; set; }
        [DisplayName("Rating ")]
        public int RatingId { get; set; }

        public double Cost { get; set; }
        [DisplayName("Quantity")]
        public int InStkQty { get; set; }

        [DisplayName("Image Path")]
        public string? ImagePath { get; set; }
        [DisplayName("Rating Description")]
        public string? RatingDescription { get; set; }


        [DisplayName("Format Description")]
        public string? FormatDescription { get; set; }

        [DisplayName("Director Name")]
        public string? FullName { get; set; }


        public List<Genre> Genre { get ; set; } = new List<Genre>();
        

    }
}
