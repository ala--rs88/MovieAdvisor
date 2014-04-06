namespace MovieAdvisor.Business.Services.Core.IICF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Core;
    using Interfaces.Core;
    using DataAccess.Interfaces.Core;

    public class RatingPredictionService : IRatingPredictionService
    {
        //// todo: IK move to settings
        private const int CountOfSimilarMoviesToUseForPrediction = 30;

        private readonly IMovieSimilarityService movieSimilarityService;

        private readonly IRatingRecordDataManager ratingRecordDataManager;

        public RatingPredictionService(
            IMovieSimilarityService movieSimilarityService,
            IRatingRecordDataManager ratingRecordDataManager)
        {
            if (movieSimilarityService == null)
            {
                throw new ArgumentNullException("movieSimilarityService");
            }

            if (ratingRecordDataManager == null)
            {
                throw new ArgumentNullException("ratingRecordDataManager");
            }

            this.movieSimilarityService = movieSimilarityService;
            this.ratingRecordDataManager = ratingRecordDataManager;
        }
        
        public double GetPrediction(int userId, int movieId)
        {
            var movieSimilarities = movieSimilarityService.GetMoviesSimilaritiesForUser(movieId, userId);

            var weightedRatingsPairs = new List<Tuple<double, RatingValueEnum>>();

            foreach (var moviesSimilarity in movieSimilarities.OrderByDescending(s => s.SimilarityValue))
            {
                if (weightedRatingsPairs.Count >= CountOfSimilarMoviesToUseForPrediction)
                {
                    break;
                }

                var rating = (RatingValueEnum)ratingRecordDataManager.GetUserRatingForMovie(userId, moviesSimilarity.TargetMovieId);

                if (rating != RatingValueEnum.Unknown)
                {
                    weightedRatingsPairs.Add(Tuple.Create(moviesSimilarity.SimilarityValue, rating));
                }
            }

            return CalculatePrediction(weightedRatingsPairs);
        }

        public double CalculatePrediction(IList<Tuple<double, RatingValueEnum>> weightedRatingsPairs)
        {
            if (weightedRatingsPairs == null)
            {
                throw new ArgumentNullException("weightedRatingsPairs");
            }

            if (!weightedRatingsPairs.Any())
            {
                return GetBaselinePrediction();
            }

            var totalWeight = weightedRatingsPairs.Sum(p => Math.Abs(p.Item1));

            var weightedRatingsSum = weightedRatingsPairs.Sum(p => p.Item1 * (int)p.Item2);

            var predictionValue = weightedRatingsSum / totalWeight;

            return predictionValue;
        }

        public double GetBaselinePrediction()
        {
            //// todo: IK implement baseline predictions
            return -1;
        }
    }
}
