namespace MovieAdvisor.Business.Services.Core.IICF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Core.RatingPrediction;
    using Interfaces.Core;
    using DataAccess.Interfaces.Core;

    public class CsMovieSimilarityService : IMovieSimilarityService
    {
        private readonly IRatingRecordDataManager ratingRecordDataManager;

        private readonly IMovieSimilarityDataService movieSimilarityDataService;

        public CsMovieSimilarityService(
            IRatingRecordDataManager ratingRecordDataManager,
            IMovieSimilarityDataService movieSimilarityDataService)
        {
            if (ratingRecordDataManager == null)
            {
                throw new ArgumentNullException("ratingRecordDataManager");
            }

            if (movieSimilarityDataService == null)
            {
                throw new ArgumentNullException("movieSimilarityDataService");
            }

            this.ratingRecordDataManager = ratingRecordDataManager;
            this.movieSimilarityDataService = movieSimilarityDataService;
        }

        public IEnumerable<MoviesSimilarity> GetMoviesSimilaritiesForUser(int sourceMovieId, int userId)
        {
            if (sourceMovieId < 1)
            {
                throw new ArgumentOutOfRangeException("sourceMovieId");
            }

            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException("userId");
            }

            var similarityTargetsCandidates = ratingRecordDataManager.GetMoviesIdsRatedByUser(userId).Where(i => i != sourceMovieId).ToList();

            var moviesSimilarities = similarityTargetsCandidates.Select(targetMovieId => GetMoviesSimilarity(sourceMovieId, targetMovieId)).ToList();

            return moviesSimilarities;
        }

        public MoviesSimilarity GetMoviesSimilarity(int sourceMovieId, int targetMovieId)
        {
            if (sourceMovieId < 1)
            {
                throw new ArgumentOutOfRangeException("sourceMovieId");
            }

            if (targetMovieId < 1)
            {
                throw new ArgumentOutOfRangeException("targetMovieId");
            }

            var moviesRatingsVectorProduct = movieSimilarityDataService.GetMoviesRatingsVectorProduct(sourceMovieId, targetMovieId);

            var sourceMovieRatingsNorm = movieSimilarityDataService.GetMovieRatingsNorm(sourceMovieId);
            var targetMovieRatingsNorm = movieSimilarityDataService.GetMovieRatingsNorm(targetMovieId);

            if (sourceMovieRatingsNorm.Equals(0) || targetMovieRatingsNorm.Equals(0))
            {
                return new MoviesSimilarity
                {
                    SourceMovieId = sourceMovieId,
                    TargetMovieId = targetMovieId,
                    SimilarityValue = 0
                };
            }

            var similarityIndex = moviesRatingsVectorProduct / (sourceMovieRatingsNorm * targetMovieRatingsNorm);

            return new MoviesSimilarity
            {
                SourceMovieId = sourceMovieId,
                TargetMovieId = targetMovieId,
                SimilarityValue = similarityIndex
            };
        }
    }
}
