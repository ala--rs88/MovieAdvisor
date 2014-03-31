using Microsoft.Practices.ObjectBuilder2;

namespace MovieAdvisor.Common.Unity
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.Unity;

    public abstract class AbstractCompositeUnityRegistry : IUnityRegistry
    {
        public UnityRegistryApplicationOrderEnum ApplicationOrder
        {
            get
            {
                return UnityRegistryApplicationOrderEnum.ShouldBeAppliedLastOrOrderIsNotSpecified;
            }
        }

        protected abstract IEnumerable<IUnityRegistry> InnerRegistries { get; }

        public void ApplyToUnityContainer(IUnityContainer container)
        {
            InnerRegistries
                .OrderBy(r => r.ApplicationOrder)
                .ForEach(r => r.ApplyToUnityContainer(container));
        } 
    }
}
