namespace MovieAdvisor.DataAccess.Services.Core
{
    using Entities.Core;
    using Interfaces.Core;
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
