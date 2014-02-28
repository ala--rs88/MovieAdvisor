namespace MovieAdvisor.Core.DataAccess
{
    using Entities;
    using Interfaces;
    using Neo4jClient;

    public class RatingRecordDataManager : BaseDataManager, IRatingRecordDataManager
    {
        public RatingRecordDataManager(IGraphClient neo4jClient)
            : base(neo4jClient)
        {
        }

        public void CreateRatingRecord(RatingRecordData ratingRecordData)
        {
            Neo4j.Cypher
                .Match("(u:User), (m:Movie)")
                .Where((UserData u) => u.UserId == ratingRecordData.UserId)
                .AndWhere((MovieData m) => m.MovieId == ratingRecordData.MovieId)
                .CreateUnique("u-[:Rated {ratingProperties}]->m")
                .WithParam("ratingProperties", new { RatingValue = ratingRecordData.Value, ratingRecordData.Timestamp })
                .ExecuteWithoutResults();
        }
    }
}
