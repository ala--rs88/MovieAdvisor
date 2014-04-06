namespace MovieAdvisor.Business.Services.Facades
{
    using System;
    using Interfaces.Core;
    using Interfaces.Facades;

    public class MovieAdvisorFacade : IMovieAdvisorFacade
    {
        private readonly IRatingPredictionService ratingPredictionService;

        public MovieAdvisorFacade(IRatingPredictionService ratingPredictionService)
        {
            if (ratingPredictionService == null)
            {
                throw new ArgumentNullException("ratingPredictionService");
            }

            this.ratingPredictionService = ratingPredictionService;
        }

        public double GetRatingPrediction(int userId, int movieId)
        {
            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException("userId");
            }

            if (movieId < 1)
            {
                throw new ArgumentOutOfRangeException("movieId");
            }

            return ratingPredictionService.GetPrediction(userId, movieId);
        }
    }
}
