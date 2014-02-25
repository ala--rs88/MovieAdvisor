using MovieAdvisor.Core.DataAccess.Entities;

namespace MovieAdvisor.BulkDataLoader.Tests.EntityParsers
{
    using Core.Server.EntityParsers;
    using NUnit.Framework;

    [TestFixture]
    public class RatingRecordParserTests
    {
        private RatingRecordParser ratingRecordParser;

        [SetUp]
        public void SetUp()
        {
            ratingRecordParser = new RatingRecordParser();
        }

        [Test]
        [TestCase("1::1287::5::978302039", 1, 1287, RatingValueEnumData.FiveStars, 978302039)]
        public void ParseFromStringTest(string inputString, int userId, int movieId, RatingValueEnumData ratingValue, int timestamp)
        {
            var result = ratingRecordParser.ParseFromString(inputString);

            Assert.NotNull(result, "result is null");
            Assert.AreEqual(userId, result.UserId, "UserId");
            Assert.AreEqual(movieId, result.MovieId, "MovieId");
            Assert.AreEqual(ratingValue, result.Value, "ratingValue");
            Assert.AreEqual(timestamp, result.Timestamp, "timestamp");
        }
    }
}
