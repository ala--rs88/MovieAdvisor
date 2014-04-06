namespace MovieAdvisor.Business.Interfaces.Core
{
    using System.Collections.Generic;
    using Entities.Core.RatingPrediction;

    public interface IMovieSimilarityService
    {
        MoviesSimilarity GetMoviesSimilarity(int sourceMovieId, int targetMovieId);

        IEnumerable<MoviesSimilarity> GetMoviesSimilaritiesForUser(int sourceMovieId, int userId);
    }
}