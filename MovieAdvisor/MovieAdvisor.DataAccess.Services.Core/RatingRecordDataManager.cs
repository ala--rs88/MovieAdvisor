namespace MovieAdvisor.DataAccess.Services.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Core;
    using Interfaces.Core;
    using Neo4jClient;

    public class RatingRecordDataManager : BaseDataManager, IRatingRecordDataManager
    {
        public RatingRecordDataManager(IGraphClient neo4jClient)
            : base(neo4jClient)
        {
        }

        public void CreateRatingRecord(RatingRecordData ratingRecordData)
        {
            if (ratingRecordData == null)
            {
                throw new ArgumentNullException("ratingRecordData");
            }

            Neo4j.Cypher
                .Match("(u:User), (m:Movie)")
                .Where((UserData u) => u.UserId == ratingRecordData.UserId)
                .AndWhere((MovieData m) => m.MovieId == ratingRecordData.MovieId)
                .CreateUnique("u-[:Rated {ratingProperties}]->m")
                .WithParam("ratingProperties", new { RatingValue = ratingRecordData.Value, ratingRecordData.Timestamp })
                .ExecuteWithoutResults();
        }

        public IEnumerable<int> GetMoviesIdsRatedByUser(int userId)
        {
            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException("userId");
            }

            return Neo4j.Cypher
                .Match("(u:User)-[:Rated]->(m:Movie)")
                .Where((UserData u) => u.UserId == userId)
                .Return(m => m.As<MovieData>().MovieId)
                .Results;
        }

        public RatingValueEnumData GetUserRatingForMovie(int userId, int movieId)
        {
            if (userId < 1)
            {
                throw new ArgumentOutOfRangeException("userId");
            }

            if (movieId < 1)
            {
                throw new ArgumentOutOfRangeException("movieId");
            }

            var ratingValue = Neo4j.Cypher
                .Match("(u:User)-[r:Rated]->(m:Movie)")
                .Where((UserData u) => u.UserId == 3)
                .AndWhere((MovieData m) => m.MovieId == 4)
                .Return(r => r.As<RatingRecordData>().Value)
                .Results
                .SingleOrDefault();

            return ratingValue;
        }
    }
}
