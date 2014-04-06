namespace MovieAdvisor.Business.Unity.Core
{
    using Microsoft.Practices.Unity;
    using Interfaces.Core;
    using Interfaces.Facades;
    using Services.Core.IICF;
    using Services.Facades;
    using Common.Unity;

    public class BusinessBaseUnityRegistry : IUnityRegistry
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
            container.RegisterType<IMovieAdvisorFacade, MovieAdvisorFacade>(new PerResolveLifetimeManager());

            container.RegisterType<IRatingPredictionService, RatingPredictionService>(new PerResolveLifetimeManager());
            container.RegisterType<IMovieSimilarityService, CsMovieSimilarityService>(new PerResolveLifetimeManager());
        }
    }
}
