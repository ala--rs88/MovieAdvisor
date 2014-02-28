namespace MovieAdvisor.Core.DataAccess
{
    using Entities;
    using Interfaces;
    using Neo4jClient;

    public class UserDataManager : BaseDataManager, IUserDataManager
    {
        public UserDataManager(IGraphClient neo4jClient)
            : base(neo4jClient)
        {
        }

        public void CreateUser(UserData userData)
        {
            Neo4j.Cypher
                .Create("(:User {newUser})")
                .WithParam("newUser", userData)
                .ExecuteWithoutResults();
        }
    }
}
