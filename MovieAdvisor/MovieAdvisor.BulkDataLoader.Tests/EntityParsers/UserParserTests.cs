namespace MovieAdvisor.BulkDataLoader.Tests.EntityParsers
{
    using Workflow.EntityParsers;
    using DataAccess.Entities.Core;
    using NUnit.Framework;

    [TestFixture]
    public class UserParserTests
    {
        private UserParser userParser;

        [SetUp]
        public void SetUp()
        {
            userParser = new UserParser();
        }

        [Test]
        [TestCase("14::M::35::0::60126", 14, GenderEnumData.Male, AgeRangeEnumData.From35To44, UserOccupationEnumData.Unknown, "60126")]
        [TestCase("17::F::18::12::60-126", 17, GenderEnumData.Female, AgeRangeEnumData.From18To24, UserOccupationEnumData.Programmer, "60-126")]
        public void ParseFromStringTest(
            string inputString,
            int userId,
            GenderEnumData gender,
            AgeRangeEnumData ageRange,
            UserOccupationEnumData occupation,
            string zipCode)
        {
            var result = userParser.ParseFromString(inputString);

            Assert.NotNull(result, "result is null");
            Assert.AreEqual(userId, result.UserId, "UserId");
            Assert.AreEqual(gender, result.Gender, "Gender");
            Assert.AreEqual(ageRange, result.AgeRange, "AgeRange");
            Assert.AreEqual(occupation, result.Occupation, "Occupation");
            Assert.AreEqual(zipCode, result.ZipCode, "ZipCode");
        }
    }
}
