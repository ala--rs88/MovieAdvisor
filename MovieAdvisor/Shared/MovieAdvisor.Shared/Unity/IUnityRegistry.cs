namespace MovieAdvisor.Shared.Unity
{
    using Microsoft.Practices.Unity;

    public interface IUnityRegistry
    {
        /// <summary>
        /// The apply to unity container.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        void ApplyToUnityContainer(IUnityContainer container);
    }
}