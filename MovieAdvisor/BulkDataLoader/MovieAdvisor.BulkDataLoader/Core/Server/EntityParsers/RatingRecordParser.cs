using MovieAdvisor.BulkDataLoader.Core.Entities.Exceptions;

namespace MovieAdvisor.BulkDataLoader.Core.Server.EntityParsers
{
    using System;
    using System.Globalization;
    using Interfaces;
    using MovieAdvisor.Core.DataAccess.Entities;

    public class RatingRecordParser : IEntityParser<RatingRecordData>
    {
        /// <exception cref="EntityParserException">Any inner exception is wrapped into <c>EntityParserException</c> instance before throwing.</exception>
        public RatingRecordData ParseFromString(string stringRepresentation)
        {
            try
            {
                var values = stringRepresentation.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);

                var userId = int.Parse(values[0]);
                var movieId = int.Parse(values[1]);

                var ratingValue = (RatingValueEnumData)int.Parse(values[2]);

                if (ratingValue == RatingValueEnumData.Unknown || !Enum.IsDefined(typeof(RatingValueEnumData), ratingValue))
                {
                    throw new FormatException();
                }

                var timestamp = int.Parse(values[3]);

                return new RatingRecordData
                {
                    UserId = userId,
                    MovieId = movieId,
                    Value = ratingValue,
                    Timestamp = timestamp
                };
            }
            catch (Exception ex)
            {
                throw new EntityParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse {0} from string: {1}", typeof(RatingRecordData).Name, stringRepresentation), ex);
            }
        }
    }
}
