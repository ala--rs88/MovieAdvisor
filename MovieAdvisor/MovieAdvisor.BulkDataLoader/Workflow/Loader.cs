using MovieAdvisor.BulkDataLoader.Entities.Exceptions;
using MovieAdvisor.BulkDataLoader.Interfaces;
using MovieAdvisor.DataAccess.Entities.Core;

namespace MovieAdvisor.BulkDataLoader.Core.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Practices.ObjectBuilder2;
    using DataAccess;
    using Properties;

    public class Loader
    {
        private readonly IEntityParser<UserData> userParser;

        private readonly IEntityParser<MovieData> movieParser;

        private readonly IEntityParser<RatingRecordData> ratingRecordParser;

        private readonly ITargetStorageDataManager targetStorageDataManager;

        public Loader(
            IEntityParser<UserData> userParser,
            IEntityParser<MovieData> movieParser,
            IEntityParser<RatingRecordData> ratingRecordParser,
            ITargetStorageDataManager targetStorageDataManager)
        {
            // todo: consider using AOP for defensive programming
            if (userParser == null)
            {
                throw new ArgumentNullException("userParser");
            }

            if (movieParser == null)
            {
                throw new ArgumentNullException("movieParser");
            }

            if (ratingRecordParser == null)
            {
                throw new ArgumentNullException("ratingRecordParser");
            }

            if (targetStorageDataManager == null)
            {
                throw new ArgumentNullException("targetStorageDataManager");
            }

            this.userParser = userParser;
            this.movieParser = movieParser;
            this.ratingRecordParser = ratingRecordParser;

            this.targetStorageDataManager = targetStorageDataManager;

            Console.WriteLine("Loader instance created.");
            Console.WriteLine();
        }

        // todo: add time measurment and estimating and counters: RawItemsCount, ParsedCount, LoadedCount
        public void LoadEntities()
        {
            if (Settings.Default.AreUsersLoaded)
            {
                LoadUsersByBatches(Settings.Default.UsersDataPath);
            }
            else
            {
                Console.WriteLine("[{0}] Loader: users loading disabled.", DateTime.Now);
                Console.WriteLine();
            }

            if (Settings.Default.AreMoviesLoaded)
            {
                LoadMoviesByBatches(Settings.Default.MoviesDataPath);
            }
            else
            {
                Console.WriteLine("[{0}] Loader: movies loading disabled.", DateTime.Now);
                Console.WriteLine();
            }

            if (Settings.Default.AreRatingRecordsLoaded)
            {
                LoadRatingsByBatches(Settings.Default.RatingsDataPath);
            }
            else
            {
                Console.WriteLine("[{0}] Loader: ratings loading disabled.", DateTime.Now);
                Console.WriteLine();
            }
        }

        private static void ParseRawDataFile(string filePath, Action<string> perLineAction)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    try
                    {
                        perLineAction(line);
                    }
                    catch (EntityParserException)
                    {
                        Console.WriteLine("[{0}] Loader: Failed to parse line '{1}' in file {2}.", DateTime.Now, line, filePath);
                    }
                    catch
                    {
                        Console.WriteLine("[{0}] Loader: Failed to load line '{1}' parsed from file {2}.", DateTime.Now, line, filePath);
                    }
                }
            }
        }

        private static IEnumerable<IEnumerable<T>> SplitIntoBatches<T>(IEnumerable<T> source, int batchSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / batchSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        private void LoadUsersByBatches(string usersDataPath)
        {
            // todo: use AOP nstead
            // todo: replace Console with custom logger wrapper: ConsoleLogger: ILogger
            Console.WriteLine("[{0}] Loader: users loading started.", DateTime.Now);

            var users = new List<UserData>();

            ParseRawDataFile(
                usersDataPath,
                entityStringRepresentation =>
                {
                    var parsedUser = userParser.ParseFromString(entityStringRepresentation);
                    users.Add(parsedUser);
                });

            SplitIntoBatches(users, 700).ForEach(batch => targetStorageDataManager.CreateUsersByBatch(batch));

            Console.WriteLine("[{0}] Loader: users loading finished.", DateTime.Now);
            Console.WriteLine();
        }


        private void LoadMoviesByBatches(string moviesDataPath)
        {
            Console.WriteLine("[{0}] Loader: movies loading started.", DateTime.Now);

            var movies = new List<MovieData>();

            ParseRawDataFile(
                moviesDataPath,
                entityStringRepresentation =>
                {
                    var parsedMovie = movieParser.ParseFromString(entityStringRepresentation);
                    movies.Add(parsedMovie);
                });

            SplitIntoBatches(movies, 700).ForEach(batch => targetStorageDataManager.CreateMoviesByBatch(batch));

            Console.WriteLine("[{0}] Loader: movies loading finished.", DateTime.Now);
            Console.WriteLine();
        }

        private void LoadRatingsByBatches(string ratingsDataPath)
        {
            Console.WriteLine("[{0}] Loader: ratings loading started.", DateTime.Now);

            var ratings = new List<RatingRecordData>();
            
            ParseRawDataFile(
                ratingsDataPath,
                entityStringRepresentation =>
                {
                    var parsedRatingRecord = ratingRecordParser.ParseFromString(entityStringRepresentation);
                    ratings.Add(parsedRatingRecord);
                });

            SplitIntoBatches(ratings, 3).ForEach(batch => targetStorageDataManager.CreateRatingRecordsByBatch(batch));

            Console.WriteLine("[{0}] Loader: ratings loading finished.", DateTime.Now);
            Console.WriteLine();
        }
    }
}
