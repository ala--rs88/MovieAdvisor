namespace MovieAdvisor.DataAccess.Interfaces.Core
{
    using Entities.Core;

    public interface IMovieDataManager
    {
        void CreateMovie(MovieData movieData);

        //// toto: add overload for *Draft 
    }
}