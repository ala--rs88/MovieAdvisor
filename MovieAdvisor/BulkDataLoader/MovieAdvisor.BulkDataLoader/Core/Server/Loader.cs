using System.Diagnostics;
using System.Globalization;
using Neo4jClient.Cypher;

namespace MovieAdvisor.BulkDataLoader.Core.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entities.Exceptions;
    using Interfaces;
    using Properties;
    using MovieAdvisor.Core.DataAccess;
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
        public void LoadEntities()
        {
            var sw = new Stopwatch();

            if (Settings.Default.AreUsersLoaded)
            {
                sw.Reset();
                sw.Start();
                LoadUsersByBatches(Settings.Default.UsersDataPath);
                sw.Stop();
                Console.WriteLine("Time spent: {0}", sw.Elapsed);
            }
            else
            {
                Console.WriteLine("[{0}] Loader: users loading disabled.", DateTime.Now);
                Console.WriteLine();
            }

            if (Settings.Default.AreMoviesLoaded)
            {
                sw.Reset();
                sw.Start();
                LoadMoviesByBatches(Settings.Default.MoviesDataPath);
                sw.Stop();
                Console.WriteLine("Time spent: {0}", sw.Elapsed);
            }
            else
            {
                Console.WriteLine("[{0}] Loader: movies loading disabled.", DateTime.Now);
                Console.WriteLine();
            }

            if (Settings.Default.AreRatingRecordsLoaded)
            {
                sw.Reset();
                sw.Start();
                LoadRatingsByBatches(Settings.Default.RatingsDataPath);
                sw.Stop();
                Console.WriteLine("Time spent: {0}", sw.Elapsed);
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

        public static List<List<T>> SplitIntoChunks<T>(List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        private static ICypherFluentQuery ApplyWhereStatements(ICypherFluentQuery initialQuery, int numberOfTimes)
        {
            if (numberOfTimes < 1)
            {
                throw new ArgumentOutOfRangeException("numberOfTimes");
            }

            var whereQuery = initialQuery.Where("u0.UserId = {u0Id}").AndWhere("m0.MovieId = {m0Id}");


            for (int i = 1; i < numberOfTimes; i++)
            {
                whereQuery = whereQuery
                    .AndWhere(string.Format(CultureInfo.InvariantCulture, "u{0}.UserId = {{u{0}Id}}", i))
                    .AndWhere(string.Format(CultureInfo.InvariantCulture, "m{0}.MovieId = {{m{0}Id}}", i));
            }

            return whereQuery;
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

        private void LoadUsersByBatches(string usersDataPath)
        {
            Console.WriteLine("[{0}] Loader: users loading started.", DateTime.Now);

            var users = new List<UserData>();

            ParseRawDataFile(
                usersDataPath,
                entityStringRepresentation =>
                {
                    var parsedUser = userParser.ParseFromString(entityStringRepresentation);
                    users.Add(parsedUser);
                });

            var neo4j = new Neo4jFactory().CreateNeo4jClient();

            SplitIntoChunks(users, 700).ForEach(chunk => neo4j.Cypher
                .Create("(:User {users})")
                .WithParam("users", chunk.ToArray())
                .ExecuteWithoutResults());

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
            
            var neo4j = new Neo4jFactory().CreateNeo4jClient();

            SplitIntoChunks(movies, 700).ForEach(chunk => neo4j.Cypher
                .Create("(:Movie {movies})")
                .WithParam("movies", chunk.ToArray())
                .ExecuteWithoutResults());

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

            var neo4j = new Neo4jFactory().CreateNeo4jClient();

            const int chunkSize = 3;

            SplitIntoChunks(ratings, chunkSize).ForEach(chunk =>
            {
                var sw = new Stopwatch();
                sw.Start();

                var matchStatement = chunk.Select(
                    (r, i) => string.Format(CultureInfo.InvariantCulture, "(u{0}:User),(m{0}:Movie)", i))
                    .Aggregate((a, b) => a + "," + b);

                var createStatement = chunk.Select(
                    (r, i) => string.Format(CultureInfo.InvariantCulture, "u{0}-[:Rated {{ratingProperties{0}}}]->m{0}", i))
                    .Aggregate((a, b) => a + "," + b);

                var parameters = new Dictionary<string, object>();

                chunk.SelectMany((r, i) => new[]
                {
                    new Tuple<string, object>(string.Format(CultureInfo.InvariantCulture, "ratingProperties{0}", i), new { RatingValue = r.Value, r.Timestamp }), 
                    new Tuple<string, object>(string.Format(CultureInfo.InvariantCulture, "u{0}Id", i), r.UserId),
                    new Tuple<string, object>(string.Format(CultureInfo.InvariantCulture, "m{0}Id", i), r.MovieId)
                }).ToList().ForEach(tuple => parameters.Add(tuple.Item1, tuple.Item2));

                var matchQuery = neo4j.Cypher.Match(matchStatement);

                var whereQuery = ApplyWhereStatements(matchQuery, chunkSize);
                
                whereQuery
                    .CreateUnique(createStatement)
                    .WithParams(parameters)
                    .ExecuteWithoutResults();

                sw.Stop();
                Console.WriteLine("ITERATION LASTED: {0}", sw.Elapsed);
            });

            Console.WriteLine("[{0}] Loader: ratings loading finished.", DateTime.Now);
            Console.WriteLine();
        }
    }
}
