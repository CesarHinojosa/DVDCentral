using CH.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CH.DVDCentral.UI.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of Customers";
            return View(CustomerManager.Load());
        }

        public IActionResult Details(int id)
        {
            var item = CustomerManager.LoadById(id);
            ViewBag.Title = "Details for " + item.FirstName + " " + item.LastName;
            return View(item);
        }

        public IActionResult Create(string returnUrl)
        {
            ViewBag.Title = "Create a Customer";
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                TempData["returnUrl"] = returnUrl;
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
        public IActionResult Create(Customer customer)
        {
            try
            {
                customer.UserID = HttpContext.Session.GetObject<User>("user").Id;

                int result = CustomerManager.Insert(customer);

                if (TempData["returnUrl"] != null)
                {
                    return Redirect(TempData["returnUrl"]?.ToString());
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {

            ViewBag.Title = "Edit a Customer";
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View(CustomerManager.LoadById(id));
            }
            else
            {
                //first is action result
                //second is the controller
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
            
        }

        [HttpPost]
        public IActionResult Edit(int id, Customer customer, bool rollback = false)
        {
            try
            {
                int result = CustomerManager.Update(customer, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);

            }
        }


        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete";
            return View(CustomerManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Customer customer, bool rollback = false)
        {
            try
            {
                int result = CustomerManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);

            }
        }
    }
}
