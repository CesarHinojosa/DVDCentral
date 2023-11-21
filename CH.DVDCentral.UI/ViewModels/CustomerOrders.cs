namespace CH.DVDCentral.UI.ViewModels
{
    public class CustomerOrders
    {
        public List<Customer> Customer { get; set; } = new List<Customer>();

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
