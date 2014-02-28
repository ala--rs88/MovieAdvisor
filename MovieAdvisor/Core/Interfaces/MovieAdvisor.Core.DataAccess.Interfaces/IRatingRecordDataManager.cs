namespace MovieAdvisor.Core.DataAccess.Interfaces
{
    using Entities;

    public interface IRatingRecordDataManager
    {
        void CreateRatingRecord(RatingRecordData ratingRecordData);

        //// toto: add overload for *Draft 
    }
}