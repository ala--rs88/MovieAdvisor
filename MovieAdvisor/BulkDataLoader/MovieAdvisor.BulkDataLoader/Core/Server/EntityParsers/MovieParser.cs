namespace MovieAdvisor.BulkDataLoader.Core.Server.EntityParsers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Exceptions;
    using MovieAdvisor.Core.DataAccess.Entities;

    public class MovieParser
    {
        private readonly Dictionary<string, MovieGenreFlagsEnumData> genresRepresentationMap = new Dictionary<string, MovieGenreFlagsEnumData>
        {
            { "Action", MovieGenreFlagsEnumData.Action },
            { "Adventure", MovieGenreFlagsEnumData.Adventure },
            { "Animation", MovieGenreFlagsEnumData.Animation },
            { "Children's", MovieGenreFlagsEnumData.Children },
            { "Comedy", MovieGenreFlagsEnumData.Comedy },
            { "Crime", MovieGenreFlagsEnumData.Crime },
            { "Documentary", MovieGenreFlagsEnumData.Documentary },
            { "Drama", MovieGenreFlagsEnumData.Drama },
            { "Fantasy", MovieGenreFlagsEnumData.Fantasy },
            { "Film-Noir", MovieGenreFlagsEnumData.FilmNoir },
            { "Horror", MovieGenreFlagsEnumData.Horror },
            { "Musical", MovieGenreFlagsEnumData.Musical },
            { "Mystery", MovieGenreFlagsEnumData.Mystery },
            { "Romance", MovieGenreFlagsEnumData.Romance },
            { "Sci-Fi", MovieGenreFlagsEnumData.SciFi },
            { "Thriller", MovieGenreFlagsEnumData.Thriller },
            { "War", MovieGenreFlagsEnumData.War },
            { "Western", MovieGenreFlagsEnumData.Western }
        };

        /// <exception cref="EntityParserException">Any inner exception is wrapped into <c>EntityParserException</c> instance before throwing.</exception>
        public MovieData ParseFromString(string stringRepresentation)
        {
            // todo: refactor
            try
            {
                var values = stringRepresentation.Split(new[] {"::"}, StringSplitOptions.RemoveEmptyEntries);
                var id = int.Parse(values[0]);

                var openBraceIndex = values[1].LastIndexOf('(');

                var title = values[1].Substring(0, openBraceIndex - 1);
                var year = int.Parse(values[1].Substring(openBraceIndex + 1, 4));

                var genres = values[2].Split('|').Select(x => genresRepresentationMap[x]).Aggregate((a, b) => a | b);

                return new MovieData
                {
                    MovieId = id,
                    Title = title,
                    Year = year,
                    Genres = genres
                };
            }
            catch (Exception ex)
            {
                throw new EntityParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse {0} from string: {1}", typeof(MovieData).Name, stringRepresentation), ex);
            }
        }
    }
}
