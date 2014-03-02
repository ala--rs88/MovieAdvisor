namespace MovieAdvisor.BulkDataLoader
{
    using Microsoft.Practices.Unity;
    using Core.Server;
    using UnityConfiguration;

    public class Program
    {
        public static void Main(string[] args)
        {
            // todo: implement some StatusBar-like feature.

            // todo: wrap container into DependencyResolver to hide excess interface.
            var unityContainer = new UnityContainer();
            new CompositeRegistry().ApplyToUnityContainer(unityContainer);

            var loader = unityContainer.Resolve<Loader>();

            loader.LoadEntities();
        }
    }
}
