namespace MovieAdvisor.BulkDataLoader
{
    using System;
    using Microsoft.Practices.Unity;
    using Core.Server;
    using Properties;
    using MovieAdvisor.Core.DataAccess.UnityConfiguration;

    public class Program
    {
        public static void Main(string[] args)
        {
            // todo: implement some StatusBar-like feature.

            // todo: wrap container into DependencyResolver to hide excess interface.
            var unityContainer = new UnityContainer();
            new DataAccessBaseUnityRegistry().ApplyToUnityContainer(unityContainer);

            var loader = unityContainer.Resolve<Loader>();

            Console.WriteLine("Loader instance created.");
            Console.WriteLine();

            Console.WriteLine("[{0}] Loader: movies loading started.", DateTime.Now);
            loader.LoadMovies(Settings.Default.MoviesDataPath);
            // todo: add time measuring and counters: RawItemsCount, ParsedCount, LoadedCount
            Console.WriteLine("[{0}] Loader: movies loading finished.", DateTime.Now);
            Console.WriteLine();

            Console.WriteLine("[{0}] Loader: users loading started.", DateTime.Now);
            loader.LoadUsers(Settings.Default.UsersDataPath);
            Console.WriteLine("[{0}] Loader: users loading finished.", DateTime.Now);
            Console.WriteLine();

            Console.WriteLine("[{0}] Loader: ratings loading started.", DateTime.Now);
            loader.LoadRatings(Settings.Default.RatingsDataPath);
            Console.WriteLine("[{0}] Loader: ratings loading finished.", DateTime.Now);
            Console.WriteLine();
        }
    }
}
