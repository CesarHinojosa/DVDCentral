using CH.DVDCentral.BL.Models;
namespace CH.DVDCentral.UI.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        public List<Customer> Customer { get; set;} = new List<Customer>();

        public int UserId { get; set; }

        public ShoppingCart Cart { get; set; } = new ShoppingCart();
    }
}
