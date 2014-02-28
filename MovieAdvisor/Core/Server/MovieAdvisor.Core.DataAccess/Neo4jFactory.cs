using System;
using Neo4jClient;

namespace MovieAdvisor.Core.DataAccess
{
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
