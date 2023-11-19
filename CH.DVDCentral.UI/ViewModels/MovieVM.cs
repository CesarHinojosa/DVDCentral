namespace CH.DVDCentral.UI.ViewModels
{
    public class MovieVM
    {
        public BL.Models.Movie Movie { get; set; }

        public List<Genre> GenreList { get; set; } = new List<Genre>();

        public List<Director> DirectorList { get; set; } = new List<Director>();

        public List<Rating> RatingList { get; set; } = new List<Rating>();

        public List<Format> FormatList { get; set; } = new List<Format>();

        
    }
}
