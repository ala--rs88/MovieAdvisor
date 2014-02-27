namespace MovieAdvisor.BulkDataLoader.Core.Server
{
    using System;
    using System.Globalization;
    using System.IO;
    using Exceptions;
    using EntityParsers;
    using MovieAdvisor.Core.DataAccess.Entities;
    using MovieAdvisor.Core.DataAccess.Interfaces;

    // todo: refactor: move creating of StreamReaders out from Loade; the same "stream reading" code is used three times;
    public class Loader
    {
        private readonly IUserDataManager userDataManager;

        private readonly IMovieDataManager movieDataManager;

        private readonly IRatingRecordDataManager ratingRecordDataManager;

        public Loader(
            IUserDataManager userDataManager,
            IMovieDataManager movieDataManager,
            IRatingRecordDataManager ratingRecordDataManager)
        {
            // todo: consider using AOP for defensive programming
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

            this.userDataManager = userDataManager;
            this.movieDataManager = movieDataManager;
            this.ratingRecordDataManager = ratingRecordDataManager;
        }

        public void LoadUsers(string usersDataPath)
        {
            var userParser = new UserParser();

            using (var streamReader = new StreamReader(usersDataPath))
            {
                string entityStringRepresentation;
                while ((entityStringRepresentation = streamReader.ReadLine()) != null)
                {
                    try
                    {
                        var parsedUser = userParser.ParseFromString(entityStringRepresentation);
                        userDataManager.CreateUser(parsedUser);
                    }
                    catch (EntityParserException)
                    {
                        Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "FAILED TO PARSE {0} from: {1}", typeof(UserData).Name, entityStringRepresentation));
                    }
                    catch
                    {
                        Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "FAILED TO SAVE {0} parsed from: {1}", typeof(UserData).Name, entityStringRepresentation));
                    }
                }
            }
        }

        public void LoadMovies(string moviesDataPath)
        {
            var movieParser = new MovieParser();

            using (var streamReader = new StreamReader(moviesDataPath))
            {
                string entityStringRepresentation;
                while ((entityStringRepresentation = streamReader.ReadLine()) != null)
                {
                    try
                    {
                        var parsedMovie = movieParser.ParseFromString(entityStringRepresentation);
                        movieDataManager.CreateMovie(parsedMovie);
                    }
                    catch (EntityParserException)
                    {
                        Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "FAILED TO PARSE {0} from: {1}", typeof(MovieData).Name, entityStringRepresentation));
                    }
                    catch
                    {
                        Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "FAILED TO SAVE {0} parsed from: {1}", typeof(MovieData).Name, entityStringRepresentation));
                    }
                }
            }
        }

        public void LoadRatings(string ratingsDataPath)
        {
            var ratingRecordParser = new RatingRecordParser();

            using (var streamReader = new StreamReader(ratingsDataPath))
            {
                string entityStringRepresentation;
                while ((entityStringRepresentation = streamReader.ReadLine()) != null)
                {
                    try
                    {
                        var parsedRatingRecord = ratingRecordParser.ParseFromString(entityStringRepresentation);
                        ratingRecordDataManager.CreateRatingRecord(parsedRatingRecord);
                    }
                    catch (EntityParserException)
                    {
                        Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "FAILED TO PARSE {0} from: {1}", typeof(RatingRecordData).Name, entityStringRepresentation));
                    }
                    catch
                    {
                        Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "FAILED TO SAVE {0} parsed from: {1}", typeof(RatingRecordData).Name, entityStringRepresentation));
                    }
                }
            }
        }
    }
}
