namespace MovieAdvisor.DataAccess.Unity.Core
{
    using Microsoft.Practices.Unity;
    using Common.Unity;
    using Interfaces.Core;
    using Services.Core;

    public class DataAccessBaseUnityRegistry : IUnityRegistry
    {
        public UnityRegistryApplicationOrderEnum ApplicationOrder
        {
            get
            {
                return UnityRegistryApplicationOrderEnum.ShouldBeAppliedInFirstStage;
            }
        }

        public void ApplyToUnityContainer(IUnityContainer container)
        {
            container.RegisterType<IUserDataManager, UserDataManager>(new PerResolveLifetimeManager());
            container.RegisterType<IMovieDataManager, MovieDataManager>(new PerResolveLifetimeManager());
            container.RegisterType<IRatingRecordDataManager, RatingRecordDataManager>(new PerResolveLifetimeManager());

            container.RegisterInstance(new Neo4jFactory().CreateNeo4jClient());
        }
    }
}
