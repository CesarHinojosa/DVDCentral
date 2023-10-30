using Microsoft.AspNetCore.Mvc;

namespace CH.DVDCentral.UI.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View(CustomerManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(CustomerManager.LoadById(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            try
            {
                int result = CustomerManager.Insert(customer);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            return View(CustomerManager.LoadById(id));
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
