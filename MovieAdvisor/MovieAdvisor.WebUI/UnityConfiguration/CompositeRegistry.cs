namespace MovieAdvisor.WebUI.UnityConfiguration
{
    using System.Collections.Generic;
    using Business.Unity.Core;
    using Common.Unity;
    using DataAccess.Unity.Core;

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