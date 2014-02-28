namespace MovieAdvisor.Shared.Unity
{
    using Microsoft.Practices.Unity;

    public interface IUnityRegistry
    {
        UnityRegistryApplicationOrderEnum ApplicationOrder { get; }

        void ApplyToUnityContainer(IUnityContainer container);
    }
}