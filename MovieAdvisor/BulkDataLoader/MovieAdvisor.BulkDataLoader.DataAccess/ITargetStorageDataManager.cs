namespace MovieAdvisor.BulkDataLoader.DataAccess
{
    using System.Collections.Generic;
    using Core.DataAccess.Entities;

    public interface ITargetStorageDataManager
    {
        void CreateUsersByBatch(IEnumerable<UserData> usersBatch);

        void CreateMoviesByBatch(IEnumerable<MovieData> moviesBatch);

        void CreateRatingRecordsByBatch(IEnumerable<RatingRecordData> ratingRecordsBatch);
    }
}