using CH.DVDCentral.BL.Models;

namespace CH.DVDCentral.UI.ViewModels
{
    public class MovieVM
    {

        
        public Movie Movie { get; set; }

        public List<Genre> GenreList { get; set; } = new List<Genre>();

        public List<Director> DirectorList { get; set; } = new List<Director>();

        public List<Rating> RatingList { get; set; } = new List<Rating>();

        public List<Format> FormatList { get; set; } = new List<Format>();


        //For the image
        public IFormFile File { get; set; }

        //For the comprison of old and new genre Ids
        public IEnumerable<int> GenreIds { get; set; } 

        public MovieVM()
        {
            GenreList = GenreManager.Load();

        }

        public MovieVM(int id)
        {
            Movie = MovieManager.LoadById(id);

            GenreList = GenreManager.Load();
            DirectorList = DirectorManager.Load();
            RatingList = RatingManager.Load();
            FormatList = FormatManager.Load();

            //The Genre is from the movie model
            GenreIds = Movie.Genre.Select(a => a.Id);
        }
    }
}
