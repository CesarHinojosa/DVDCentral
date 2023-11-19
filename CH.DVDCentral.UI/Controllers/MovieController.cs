using CH.DVDCentral.UI.Models;
using CH.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CH.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of All Movies";
            return View(MovieManager.Load());
        }

        public IActionResult Browse(int id) 
        {
            return View(nameof(Index), MovieManager.Load(id));
        }



        public IActionResult Edit(int id)
        {
            MovieVM movieVM = new MovieVM();
            movieVM.Movie = MovieManager.LoadById(id);
            movieVM.FormatList = FormatManager.Load();
            movieVM.DirectorList = DirectorManager.Load();
            movieVM.RatingList = RatingManager.Load();
            movieVM.GenreList = GenreManager.Load();
          
            

            ViewBag.Title = "Edit " + movieVM.Movie.Title;

            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View(movieVM);
            }
            else
            {
                //first is action result
                //second is the controller
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, MovieVM movieVM, bool rollback = false)
        {
            try
            {
                int result = MovieManager.Update(movieVM.Movie, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movieVM);

            }
        }


        public IActionResult Create()
        {
            ViewBag.Title = "Create a Movie";
            MovieVM movieVM = new MovieVM();

            movieVM.Movie = new BL.Models.Movie();
            movieVM.FormatList = FormatManager.Load();
            movieVM.DirectorList = DirectorManager.Load();
            movieVM.RatingList = RatingManager.Load();
            movieVM.GenreList = GenreManager.Load();

            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View(movieVM);
            }
            else
            {
                //first is action result
                //second is the controller
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]
        public IActionResult Create(MovieVM movieVM)
        {
            try
            {
                int result = MovieManager.Insert(movieVM.Movie);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete";
            return View(MovieManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Movie movie, bool rollback = false)
        {
            try
            {
                int result = MovieManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movie);

            }
        }

    }
}
