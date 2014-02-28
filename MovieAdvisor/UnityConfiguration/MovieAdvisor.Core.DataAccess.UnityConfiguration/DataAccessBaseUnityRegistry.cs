namespace MovieAdvisor.Core.DataAccess.UnityConfiguration
{
    using Microsoft.Practices.Unity;
    using Interfaces;
    using Shared.Unity;

    public class DataAccessBaseUnityRegistry : IUnityRegistry
    {
        public void ApplyToUnityContainer(IUnityContainer container)
        {
            container.RegisterType<IUserDataManager, UserDataManager>(new PerResolveLifetimeManager());
            container.RegisterType<IMovieDataManager, MovieDataManager>(new PerResolveLifetimeManager());
            container.RegisterType<IRatingRecordDataManager, RatingRecordDataManager>(new PerResolveLifetimeManager());

            container.RegisterInstance(new Neo4jFactory().CreateNeo4jClient());
        }
    }
}
