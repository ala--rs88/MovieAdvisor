namespace MovieAdvisor.WebUI.Controllers
{
    using System;
    using System.Web.Http;
    using Business.Interfaces.Facades;

    public class RatingPredictionController : ApiController
    {
        private readonly IMovieAdvisorFacade movieAdvisor;

        public RatingPredictionController(IMovieAdvisorFacade movieAdvisor)
        {
            if (movieAdvisor == null)
            {
                throw new ArgumentNullException("movieAdvisor");
            }

            this.movieAdvisor = movieAdvisor;
        }

        [HttpGet]
        public double PredictRating(int userId, int movieId)
        {
            var predictionValue = this.movieAdvisor.GetRatingPrediction(userId, movieId);

            return predictionValue;
        }
    }
}
