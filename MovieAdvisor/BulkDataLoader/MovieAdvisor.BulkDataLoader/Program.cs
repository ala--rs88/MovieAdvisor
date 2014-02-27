namespace MovieAdvisor.BulkDataLoader
{
    using System;
    using Core.Server;
    using Properties;
    using MovieAdvisor.Core.DataAccess;

    public class Program
    {
        public static void Main(string[] args)
        {
            // todo: implement some StatusBar-like feature.

            // todo: consider using Unity Container here, consider getting rid of reference to DataAccess managers.
            var loader = new Loader(new UserDataManager(), new MovieDataManager(), new RatingRecordDataManager());

            Console.WriteLine("Loader instance created.");

            loader.LoadMovies(Settings.Default.MoviesDataPath);
            // todo: add time measuring and counters: RawItemsCount, ParsedCount, LoadedCount
            Console.WriteLine("Loader: movies loaded.");

            loader.LoadUsers(Settings.Default.UsersDataPath);
            Console.WriteLine("Loader: users loaded.");

            loader.LoadRatings(Settings.Default.RatingsDataPath);
            Console.WriteLine("Loader: ratings loaded.");
        }
    }
}
