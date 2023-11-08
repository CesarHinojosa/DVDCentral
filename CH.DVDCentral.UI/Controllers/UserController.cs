using Microsoft.AspNetCore.Mvc;
using CH.DVDCentral.UI.Extensions;


namespace CH.DVDCentral.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private void SetUser(User user)
        {
            HttpContext.Session.SetObject("user", user);

            if (user != null)
            {
                HttpContext.Session.SetObject("fullname", "Welcome " + user.FullName);
            }
            else
            {
                HttpContext.Session.SetObject("fullname", string.Empty);
            }
        }

        //To log out you just have the session as null



        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                bool result = UserManager.Login(user);
                SetUser(user);
                if (TempData["returnUrl"] != null)
                {
                    return Redirect(TempData["returnUrl"]?.ToString()); //going to a url a webpage 
                }
                return RedirectToAction(nameof(Index), "Customer");
            }
            catch (Exception ex)
            {

                ViewBag.Title = "Login";
                ViewBag.Error = ex.Message; //shows error
                return View(user); //comes back with the userId and password in stuff
            }
        }


        public IActionResult Logout()
        {
            ViewBag.Title = "Logout";
            SetUser(null);
            return View();
        }
    }
}
