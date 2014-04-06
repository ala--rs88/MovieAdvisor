
namespace MovieAdvisor.Business.Interfaces.Facades
{
    public interface IMovieAdvisorFacade
    {
        double GetRatingPrediction(int userId, int movieId);
    }
}