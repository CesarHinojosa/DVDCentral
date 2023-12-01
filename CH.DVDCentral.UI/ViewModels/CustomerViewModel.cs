using CH.DVDCentral.BL.Models;
namespace CH.DVDCentral.UI.ViewModels
{
    public class CustomerViewModel
    {
        public Customer customer {  get; set; }

        public List<User> users { get; set; } = new List<User>();
    }
}
