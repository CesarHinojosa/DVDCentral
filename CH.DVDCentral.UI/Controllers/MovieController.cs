using Microsoft.AspNetCore.Mvc;

namespace CH.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View(MovieManager.Load());
        }

        public IActionResult Browse(int id) 
        {
            return View(nameof(Index), MovieManager.Load(id));
        }

    }
}
