namespace TestApp.UnityConfiguration
{
    using System.Collections.Generic;
    using MovieAdvisor.Business.Unity.Core;
    using MovieAdvisor.Common.Unity;
    using MovieAdvisor.DataAccess.Unity.Core;

    public class CompositeRegistry : AbstractCompositeUnityRegistry
    {
        protected override IEnumerable<IUnityRegistry> InnerRegistries
        {
            get
            {
                return new IUnityRegistry[]
                {
                    new DataAccessBaseUnityRegistry(),
                    new BusinessBaseUnityRegistry()
                };
            }
        }
    }
}
