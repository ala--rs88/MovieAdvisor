namespace MovieAdvisor.BulkDataLoader.Tests.EntityParsers
{
    using Workflow.EntityParsers;
    using DataAccess.Entities.Core;
    using NUnit.Framework;

    [TestFixture]
    public class MovieParserTests
    {
        private MovieParser movieParser;

        [SetUp]
        public void SetUp()
        {
            movieParser = new MovieParser();
        }

        [Test]
        [TestCase("6::Heat (1995)::Action|Crime|Thriller", 6, "Heat", 1995, MovieGenreFlagsEnumData.Action | MovieGenreFlagsEnumData.Crime | MovieGenreFlagsEnumData.Thriller)]
        [TestCase("26::Othello (1995)::Drama", 26, "Othello", 1995, MovieGenreFlagsEnumData.Drama)]
        [TestCase("29::City of Lost Children, The (Cité des enfants perdus, La) (1995)::Adventure|Drama|Fantasy|Mystery|Sci-Fi", 29, "City of Lost Children, The (Cité des enfants perdus, La)", 1995, MovieGenreFlagsEnumData.Adventure | MovieGenreFlagsEnumData.Drama | MovieGenreFlagsEnumData.Fantasy | MovieGenreFlagsEnumData.Mystery | MovieGenreFlagsEnumData.SciFi)]
        [TestCase("65133::Blackadder Back & Forth (1999)::Comedy", 65133, "Blackadder Back & Forth", 1999, MovieGenreFlagsEnumData.Comedy)]
        public void ParseFromStringTest(string inputString, int id, string title, int year, MovieGenreFlagsEnumData genres)
        {
            var result = movieParser.ParseFromString(inputString);

            Assert.NotNull(result, "result is null");
            Assert.AreEqual(id, result.MovieId, "Id");
            Assert.AreEqual(title, result.Title, "title");
            Assert.AreEqual(year, result.Year, "year");
            Assert.AreEqual(genres, result.Genres, "genres");
        }
    }
}
