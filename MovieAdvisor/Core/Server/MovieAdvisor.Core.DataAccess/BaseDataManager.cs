namespace MovieAdvisor.Core.DataAccess
{
    using System;
    using Neo4jClient;

    public abstract class BaseDataManager
    {
        private readonly IGraphClient neo4jClient;

        protected BaseDataManager(IGraphClient neo4jClient)
        {
            if (neo4jClient == null)
            {
                throw new ArgumentNullException("neo4jClient");
            }

            this.neo4jClient = neo4jClient;
        }

        protected IGraphClient Neo4j
        {
            get
            {
                return neo4jClient;
            }
        }
    }
}
