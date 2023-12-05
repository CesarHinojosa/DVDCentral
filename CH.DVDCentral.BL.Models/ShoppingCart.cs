using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CH.DVDCentral.BL.Models
{
    public class ShoppingCart
    {

        //Declaration application specific - Declaration Cost
        //Don't do this in DVDCentral
        //const double ITEM_COST = 120.03;
        public List<Movie> Items { get; set; } = new List<Movie>();

        //public Movie movie { get { return movie.Id; } }

        
        public double Cost
        {
            get
            {
                return Items.Sum(i => i.Cost);
            }
        }

        //Movie movie { get { return movie.Cost }; }

        public int NumberOfItems { get { return Items.Count; } }

        //This formats teh currency for you  //could use items.sum for DVDCentral
        [DisplayFormat(DataFormatString = "{0:C}")]

        //TODO: Need to take a look into this
        public double SubTotal
        {
            get
            { return NumberOfItems * Cost; }
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax { get { return SubTotal * 0.055; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return SubTotal + Tax; } }
    }
}
