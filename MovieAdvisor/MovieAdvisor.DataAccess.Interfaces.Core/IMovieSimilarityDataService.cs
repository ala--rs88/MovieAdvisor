namespace MovieAdvisor.DataAccess.Interfaces.Core
{
    public interface IMovieSimilarityDataService
    {
        int GetMoviesRatingsVectorProduct(int sourceMovieId, int targetMovieId);

        double GetMovieRatingsNorm(int movieId);
    }
}