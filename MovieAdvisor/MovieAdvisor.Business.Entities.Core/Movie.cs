namespace MovieAdvisor.Business.Entities.Core
{
    public class Movie
    {
        // todo: create *Draft entities;
        public int MovieId { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public MovieGenreFlagsEnum Genres { get; set; }
    }
}
