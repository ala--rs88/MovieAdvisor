namespace MovieAdvisor.BulkDataLoader.UnityConfiguration.AppSpecific
{
    using Microsoft.Practices.Unity;
    using Core.Interfaces;
    using Core.Server.EntityParsers;
    using MovieAdvisor.Core.DataAccess.Entities;
    using Shared.Unity;

    public class LoaderBaseRegistry : IUnityRegistry
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
            container.RegisterInstance<IEntityParser<UserData>>(new UserParser());
            container.RegisterInstance<IEntityParser<MovieData>>(new MovieParser());
            container.RegisterInstance<IEntityParser<RatingRecordData>>(new RatingRecordParser());
        } 
    }
}
