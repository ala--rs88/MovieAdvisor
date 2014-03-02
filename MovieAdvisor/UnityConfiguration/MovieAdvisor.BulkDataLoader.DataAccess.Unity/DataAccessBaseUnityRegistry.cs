namespace MovieAdvisor.BulkDataLoader.DataAccess.Unity
{
    using Microsoft.Practices.Unity;
    using Core.DataAccess;
    using Shared.Unity;

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
            container.RegisterType<ITargetStorageDataManager, Neo4jDataManager>(new PerResolveLifetimeManager());

            container.RegisterInstance(new Neo4jFactory().CreateNeo4jClient());
        }
    }
}
