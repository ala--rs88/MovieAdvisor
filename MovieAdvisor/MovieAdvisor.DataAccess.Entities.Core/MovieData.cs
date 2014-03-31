namespace MovieAdvisor.DataAccess.Entities.Core
{
    public class MovieData
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public MovieGenreFlagsEnumData Genres { get; set; }
    }
}
