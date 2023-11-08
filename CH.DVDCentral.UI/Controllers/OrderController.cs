using CH.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CH.DVDCentral.UI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View(OrderManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(OrderManager.LoadById(id));
        }

        public IActionResult Create()
        {

            ViewBag.Title = "Create a Program";
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View();
            }
            else
            {
                //first is action result
                //second is the controller
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }


        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                int result = OrderManager.Insert(order);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {
           

            ViewBag.Title = "Edit a Order";
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View(OrderManager.LoadById(id));
            }
            else
            {
                //first is action result
                //second is the controller
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, Order order, bool rollback = false)
        {
            try
            {
                int result = OrderManager.Update(order, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(order);

            }
        }

        public IActionResult Delete(int id)
        {
            return View(OrderManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Order order, bool rollback = false)
        {
            try
            {
                int result = OrderManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(order);

            }
        }
    }
}
