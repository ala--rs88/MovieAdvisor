namespace MovieAdvisor.BulkDataLoader.Core.Server
{
    using System;
    using System.IO;
    using Entities.Exceptions;
    using Interfaces;
    using Properties;
    using MovieAdvisor.Core.DataAccess.Entities;
    using MovieAdvisor.Core.DataAccess.Interfaces;

    public class Loader
    {
        private readonly IEntityParser<UserData> userParser;

        private readonly IEntityParser<MovieData> movieParser;

        private readonly IEntityParser<RatingRecordData> ratingRecordParser;

        private readonly IUserDataManager userDataManager;

        private readonly IMovieDataManager movieDataManager;

        private readonly IRatingRecordDataManager ratingRecordDataManager;

        public Loader(
            IEntityParser<UserData> userParser,
            IEntityParser<MovieData> movieParser,
            IEntityParser<RatingRecordData> ratingRecordParser,
            IUserDataManager userDataManager,
            IMovieDataManager movieDataManager,
            IRatingRecordDataManager ratingRecordDataManager)
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

            if (userDataManager == null)
            {
                throw new ArgumentNullException("userDataManager");
            }

            if (movieDataManager == null)
            {
                throw new ArgumentNullException("movieDataManager");
            }

            if (ratingRecordDataManager == null)
            {
                throw new ArgumentNullException("ratingRecordDataManager");
            }

            this.userParser = userParser;
            this.movieParser = movieParser;
            this.ratingRecordParser = ratingRecordParser;

            this.userDataManager = userDataManager;
            this.movieDataManager = movieDataManager;
            this.ratingRecordDataManager = ratingRecordDataManager;

            Console.WriteLine("Loader instance created.");
            Console.WriteLine();
        }

        // todo: add time measuring and counters: RawItemsCount, ParsedCount, LoadedCount
        // todo: consider loading Entities in batches bypassing the transaction mechanisms to increase performance.
        public void LoadEntities()
        {
            if (Settings.Default.AreUsersLoaded)
            {
                LoadUsers(Settings.Default.UsersDataPath);
            }
            else
            {
                Console.WriteLine("[{0}] Loader: users loading disabled.", DateTime.Now);
                Console.WriteLine();
            }

            if (Settings.Default.AreMoviesLoaded)
            {
                LoadMovies(Settings.Default.MoviesDataPath);
            }
            else
            {
                Console.WriteLine("[{0}] Loader: movies loading disabled.", DateTime.Now);
                Console.WriteLine();
            }

            if (Settings.Default.AreRatingRecordsLoaded)
            {
                LoadRatings(Settings.Default.RatingsDataPath);
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

        private void LoadUsers(string usersDataPath)
        {
            Console.WriteLine("[{0}] Loader: users loading started.", DateTime.Now);
            
            ParseRawDataFile(
                usersDataPath,
                entityStringRepresentation =>
                {
                    var parsedUser = userParser.ParseFromString(entityStringRepresentation);
                    userDataManager.CreateUser(parsedUser);
                });

            Console.WriteLine("[{0}] Loader: users loading finished.", DateTime.Now);
            Console.WriteLine();
        }

        private void LoadMovies(string moviesDataPath)
        {
            // todo: use AOP nstead
            // todo: replace Console with custom logger wrapper: ConsoleLogger: ILogger
            Console.WriteLine("[{0}] Loader: movies loading started.", DateTime.Now);
            
            ParseRawDataFile(
                moviesDataPath,
                entityStringRepresentation =>
                {
                    var parsedMovie = movieParser.ParseFromString(entityStringRepresentation);
                    movieDataManager.CreateMovie(parsedMovie);
                });

            Console.WriteLine("[{0}] Loader: movies loading finished.", DateTime.Now);
            Console.WriteLine();
        }

        private void LoadRatings(string ratingsDataPath)
        {
            Console.WriteLine("[{0}] Loader: ratings loading started.", DateTime.Now);
            
            ParseRawDataFile(
                ratingsDataPath,
                entityStringRepresentation =>
                {
                    var parsedRatingRecord = ratingRecordParser.ParseFromString(entityStringRepresentation);
                    ratingRecordDataManager.CreateRatingRecord(parsedRatingRecord);
                });

            Console.WriteLine("[{0}] Loader: ratings loading finished.", DateTime.Now);
            Console.WriteLine();
        }
    }
}
