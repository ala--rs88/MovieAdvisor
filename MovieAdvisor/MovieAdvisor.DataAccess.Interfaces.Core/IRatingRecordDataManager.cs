namespace MovieAdvisor.DataAccess.Interfaces.Core
{
    using System.Collections.Generic;
    using Entities.Core;

    public interface IRatingRecordDataManager
    {
        void CreateRatingRecord(RatingRecordData ratingRecordData);

        //// toto: add overload for *Draft 

        IEnumerable<int> GetMoviesIdsRatedByUser(int userId);

        RatingValueEnumData GetUserRatingForMovie(int userId, int movieId);
    }
}