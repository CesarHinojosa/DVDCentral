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

            return View();
        }

    }
}
