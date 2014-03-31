namespace MovieAdvisor.DataAccess.Interfaces.Core
{
    using Entities.Core;

    public interface IRatingRecordDataManager
    {
        void CreateRatingRecord(RatingRecordData ratingRecordData);

        //// toto: add overload for *Draft 
    }
}