using CH.DVDCentral.BL.Models;
namespace CH.DVDCentral.UI.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        public List<Customer> Customer { get; set;}

        public int UserId { get; set; }

        public ShoppingCart Cart { get; set; }
    }
}
