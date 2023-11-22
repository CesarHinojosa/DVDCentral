using CH.DVDCentral.UI.Models;
using CH.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace CH.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {

        private readonly IWebHostEnvironment _host;

        public MovieController(IWebHostEnvironment host)
        {
            _host = host;
        }


        public IActionResult Index()
        {
            ViewBag.Title = "List of All Movies";
            return View(MovieManager.Load());
        }

        public IActionResult Details(int id)
        {
            ViewBag.Title = "Details";
            return View(MovieManager.LoadById(id));


        }

        public IActionResult Browse(int id) 
        {
            return View(nameof(Index), MovieManager.Load(id));
        }



        public IActionResult Edit(int id)
        {
          
            MovieVM movieVM = new MovieVM(id);
            movieVM.FormatList = FormatManager.Load();
            movieVM.DirectorList = DirectorManager.Load();
            movieVM.RatingList = RatingManager.Load();
            movieVM.GenreList = GenreManager.Load();



            ViewBag.Title = "Edit " + movieVM.Movie.Title;

            if (Authenticate.IsAuthenticated(HttpContext))
            {
                HttpContext.Session.SetObject("genreids", movieVM.GenreIds);
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

                if (movieVM.File != null)
                {
                    movieVM.Movie.ImagePath = movieVM.File.FileName;
                    string path = _host.WebRootPath + "\\images\\";

                    using (var stream = System.IO.File.Create(path + movieVM.File.FileName))
                    {
                        movieVM.File.CopyTo(stream);
                        ViewBag.Message = "File Uploaded Successfully...";
                    }
                }

                //Comparison stuff with the IDs
                IEnumerable<int> newGenreIds = new List<int>();
                if (movieVM.GenreIds != null)
                {
                    newGenreIds = movieVM.GenreIds;
                }

                IEnumerable<int> oldGenreIds = new List<int>();
                oldGenreIds = GetObject();

                IEnumerable<int> deletes = oldGenreIds.Except(newGenreIds);
                IEnumerable<int> adds = newGenreIds.Except(oldGenreIds);

                deletes.ToList().ForEach(d => MovieGenreManager.Delete(id, d));
                adds.ToList().ForEach(a => MovieGenreManager.Insert(id, a));
                //adds.ToList().ForEach(a => MovieGenreManager.Update(id, a));

                //HttpContext.Session.SetObject("genreids", movieVM.GenreIds);

                int result = MovieManager.Update(movieVM.Movie, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movieVM);

            }
        }

        private IEnumerable<int> GetObject()
        {
            if (HttpContext.Session.GetObject<IEnumerable<int>>("genreids") != null)
            {
                return HttpContext.Session.GetObject<IEnumerable<int>>("genreids");
            }
            else
            {
                return null;
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
                HttpContext.Session.SetObject("genreids", new List<int>());
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
        public IActionResult Create(MovieVM movieVM, int id)
        {
            try
            {

                if (movieVM.File != null)
                {
                    movieVM.Movie.ImagePath = movieVM.File.FileName;
                    string path = _host.WebRootPath + "\\images\\";

                    using (var stream = System.IO.File.Create(path + movieVM.File.FileName))
                    {
                        movieVM.File.CopyTo(stream);
                        ViewBag.Message = "File Uploaded Successfully...";
                    }
                }

                List<int> selectedGenreIds = HttpContext.Session.GetObject<List<int>>("genreids");
                movieVM.GenreIds = selectedGenreIds;


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
