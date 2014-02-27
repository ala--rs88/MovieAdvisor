namespace MovieAdvisor.Core.DataAccess.Interfaces
{
    using Entities;

    public interface IMovieDataManager
    {
        void CreateMovie(MovieData movieData);

        //// toto: add overload for *Draft 
    }
}