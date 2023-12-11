using CH.DVDCentral.UI.Models;
using CH.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CH.DVDCentral.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;

        public IActionResult Index()
        {
            ViewBag.Title = "Shopping Cart";
            cart = GetShoppingCart();
            return View(cart);
        }

            

        private ShoppingCart GetShoppingCart()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") != null)
            {
                return HttpContext.Session.GetObject<ShoppingCart>("cart");
            }
            else  
            {
                return new ShoppingCart();
            }
        }

        public IActionResult Remove(int id)
        {
            cart = GetShoppingCart();
            
            Movie movie = cart.Items.FirstOrDefault(i => i.Id == id);

            ShoppingCartManager.Remove(cart, movie);

            HttpContext.Session.SetObject("cart", cart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add(int id)
        {
            cart = GetShoppingCart();

            Movie movie = MovieManager.LoadById(id);

            ShoppingCartManager.Add(cart, movie);

            HttpContext.Session.SetObject("cart", cart);

            //Eliminates a database hit??
            return RedirectToAction(nameof(Index), "Movie");
        }

        public IActionResult Checkout()
        {
            cart = GetShoppingCart();

            ShoppingCartManager.CheckOut(cart);
            //it os null because once you check out ther eis no cart anymores
            HttpContext.Session.SetObject("cart", null);

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

        public ActionResult AssignToCustomer()
        {
            if (HttpContext.Session.GetObject<User>("user") == null)
            {
                //throws to login page
                return RedirectToAction("login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

            CustomerViewModel customerViewModel = new CustomerViewModel();

            Customer customer = new Customer();

            cart = GetShoppingCart();

            customerViewModel.Cart = cart;

            customerViewModel.UserId = HttpContext.Session.GetObject<User>("user").Id;

            customerViewModel.Customer = CustomerManager.Load(customerViewModel.UserId);

            if (customerViewModel.UserId != null)
            {
                if (customerViewModel.Customer.Count > 0) 
                {
                    customerViewModel.CustomerId = customerViewModel.Customer.FirstOrDefault().Id;
                    
                }
                  
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

            HttpContext.Session.SetObject("customerViewModel", customerViewModel);
            ViewData["ReturnUrl"] = UriHelper.GetDisplayUrl(HttpContext.Request);

            return View(customerViewModel);

        }

        [HttpPost]
        public ActionResult AssignToCustomer(CustomerViewModel customerViewModel)
        {
            try
            {
                ViewBag.Title = "Assigning to Customer";

                cart = GetShoppingCart();

                ShoppingCartManager.CheckOut(cart,customerViewModel.CustomerId, customerViewModel.UserId);

                HttpContext.Session.SetObject("cart", null);

                return View(nameof(Checkout));

            }
            catch (Exception ex)
            {
                 
                ViewBag.Error = ex.Message;
                return View(customerViewModel);
            }
        }

    }
}
