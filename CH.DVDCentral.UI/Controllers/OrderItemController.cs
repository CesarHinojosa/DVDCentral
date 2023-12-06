using Microsoft.AspNetCore.Mvc;

namespace CH.DVDCentral.UI.Controllers
{
    public class OrderItemController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of Order Items";
            return View(OrderItemManager.Load());
        }


        public IActionResult Remove(int id)
        {
            return View(OrderItemManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Remove(int id, OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = OrderItemManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(orderItem);

            }
        }
    }
}
