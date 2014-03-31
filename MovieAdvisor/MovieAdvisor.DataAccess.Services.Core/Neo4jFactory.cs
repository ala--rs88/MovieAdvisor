namespace MovieAdvisor.DataAccess.Services.Core
{
    using System;
    using Neo4jClient;

    public class Neo4jFactory
    {
        private const string Neo4jServerUri = "http://localhost:7474/db/data";

        public IGraphClient CreateNeo4jClient()
        {
            var neo4jClient = new GraphClient(new Uri(Neo4jServerUri));
            neo4jClient.Connect();

            return neo4jClient;
        }
    }
}
