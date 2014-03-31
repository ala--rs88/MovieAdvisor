using MovieAdvisor.BulkDataLoader.Interfaces;
using MovieAdvisor.BulkDataLoader.Workflow.EntityParsers;

namespace MovieAdvisor.BulkDataLoader.UnityConfiguration.AppSpecific
{
    using Microsoft.Practices.Unity;
    using Common.Unity;
    using MovieAdvisor.DataAccess.Entities.Core;

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
