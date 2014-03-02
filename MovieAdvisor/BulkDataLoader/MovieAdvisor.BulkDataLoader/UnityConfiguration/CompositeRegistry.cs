namespace MovieAdvisor.BulkDataLoader.UnityConfiguration
{
    using System.Collections.Generic;
    using DataAccess.Unity;
    using AppSpecific;
    using Shared.Unity;

    public class CompositeRegistry : AbstractCompositeUnityRegistry
    {
        protected override IEnumerable<IUnityRegistry> InnerRegistries
        {
            get
            {
                return new IUnityRegistry[]
                {
                    new DataAccessBaseUnityRegistry(),
                    new LoaderBaseRegistry()
                };
            }
        }
    }
}
