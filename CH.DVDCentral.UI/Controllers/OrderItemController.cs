using Microsoft.AspNetCore.Mvc;

namespace CH.DVDCentral.UI.Controllers
{
    public class OrderItemController : Controller
    {
        public IActionResult Index()
        {
            return View(OrderItemManager.Load());
        }
    }
}
