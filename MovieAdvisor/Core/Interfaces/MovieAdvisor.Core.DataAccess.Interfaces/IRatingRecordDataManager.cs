namespace MovieAdvisor.Core.DataAccess.Interfaces
{
    using Entities;

    public interface IRatingRecordDataManager
    {
        void CreateRatingRecord(RatingRecordData reatingRecordData);

        //// toto: add overload for *Draft 
    }
}