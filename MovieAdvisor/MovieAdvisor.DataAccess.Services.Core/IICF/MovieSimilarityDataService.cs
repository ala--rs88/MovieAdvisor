namespace MovieAdvisor.DataAccess.Services.Core.IICF
{
    using System;
    using System.Linq;
    using Entities.Core;
    using Interfaces.Core;
    using Neo4jClient;
    using Neo4jClient.Cypher;

    public class MovieSimilarityDataService : BaseDataManager, IMovieSimilarityDataService
    {
        public MovieSimilarityDataService(IGraphClient neo4jClient) 
            : base(neo4jClient)
        {
        }

        public int GetMoviesRatingsVectorProduct(int sourceMovieId, int targetMovieId)
        {
            if (sourceMovieId < 1)
            {
                throw new ArgumentOutOfRangeException("sourceMovieId");
            }

            if (targetMovieId < 1)
            {
                throw new ArgumentOutOfRangeException("targetMovieId");
            }

            return Neo4j.Cypher
                .Match("(m1:Movie)<-[r1:Rated]-(:User)-[r2:Rated]->(m2:Movie)")
                .Where((MovieData m1) => m1.MovieId == sourceMovieId)
                .AndWhere((MovieData m2) => m2.MovieId == targetMovieId)
                .Return((r1, r2) => Return.As<int>("sum(r1.Value * r2.Value)"))
                .Results.Single();
        }

        public double GetMovieRatingsNorm(int movieId)
        {
            if (movieId < 1)
            {
                throw new ArgumentOutOfRangeException("movieId");
            }

            return Neo4j.Cypher
                .Match("(m:Movie)<-[r:Rated]-(:User)")
                .Where((MovieData m) => m.MovieId == movieId)
                .Return(r => Return.As<double>("sqrt(sum(r.Value * r.Value))"))
                .Results.Single();
        }
    }
}
