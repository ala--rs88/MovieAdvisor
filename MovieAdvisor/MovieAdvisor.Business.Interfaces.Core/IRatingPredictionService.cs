namespace MovieAdvisor.Business.Interfaces.Core
{
    public interface IRatingPredictionService
    {
        double GetPrediction(int userId, int movieId);
    }
}