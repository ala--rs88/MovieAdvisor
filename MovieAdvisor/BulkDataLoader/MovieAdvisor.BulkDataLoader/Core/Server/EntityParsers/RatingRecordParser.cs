using System;
using MovieAdvisor.Core.DataAccess.Entities;

namespace MovieAdvisor.BulkDataLoader.Core.Server.EntityParsers
{
    public class RatingRecordParser
    {
        public RatingRecordData ParseFromString(string stringRepresentation)
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
    }
}
