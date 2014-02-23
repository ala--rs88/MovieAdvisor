namespace MovieAdvisor.Core.Business.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }

        public int Year { get; set; }

        public MovieGenreFlagsEnum Genres { get; set; }
    }
}
