namespace MovieAdvisor.BulkDataLoader.DataAccess
{
    using System.Collections.Generic;
    using MovieAdvisor.DataAccess.Entities.Core;

    public interface ITargetStorageDataManager
    {
        void CreateUsersByBatch(IEnumerable<UserData> usersBatch);

        void CreateMoviesByBatch(IEnumerable<MovieData> moviesBatch);

        void CreateRatingRecordsByBatch(IEnumerable<RatingRecordData> ratingRecordsBatch);
    }
}