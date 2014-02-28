namespace MovieAdvisor.Core.DataAccess
{
    using Entities;
    using Interfaces;
    using Neo4jClient;

    public class MovieDataManager : BaseDataManager, IMovieDataManager
    {
        public MovieDataManager(IGraphClient neo4jClient)
            : base(neo4jClient)
        {
        }

        public void CreateMovie(MovieData movieData)
        {
            Neo4j.Cypher
                .Create("(:Movie {newMovie})")
                .WithParam("newMovie", movieData)
                .ExecuteWithoutResults();
        }
    }
}
