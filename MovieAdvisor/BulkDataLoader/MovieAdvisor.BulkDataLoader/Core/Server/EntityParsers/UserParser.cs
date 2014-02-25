
using System;
using System.Collections.Generic;

namespace MovieAdvisor.BulkDataLoader.Core.Server.EntityParsers
{
    using MovieAdvisor.Core.DataAccess.Entities;

    public class UserParser
    {
        private readonly Dictionary<string, GenderEnumData> genderRepresentationMap = new Dictionary<string, GenderEnumData>
        {
            { "M", GenderEnumData.Male },
            { "F", GenderEnumData.Female }
        };

        private readonly Dictionary<string, AgeRangeEnumData> ageRangeRepresentationMap = new Dictionary<string, AgeRangeEnumData>
        {
            { "1", AgeRangeEnumData.Under18 },
            { "18", AgeRangeEnumData.From18To24 },
            { "25", AgeRangeEnumData.From25To34 },
            { "35", AgeRangeEnumData.From35To44 },
            { "45", AgeRangeEnumData.From45To49 },
            { "50", AgeRangeEnumData.From50To55 },
            { "56", AgeRangeEnumData.OlderThan55 }
        };

        public UserData ParseFromString(string stringRepresentation)
        {
            var values = stringRepresentation.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);

            var userId = int.Parse(values[0]);
            var gender = genderRepresentationMap[values[1]];
            var ageRange = ageRangeRepresentationMap[values[2]];

            var occupation = (UserOccupationEnumData)int.Parse(values[3]);

            if (!Enum.IsDefined(typeof(UserOccupationEnumData), occupation))
            {
                throw new FormatException();
            }

            var zipCode = values[4];

            return new UserData
            {
                UserId = userId,
                Gender = gender,
                AgeRange = ageRange,
                Occupation = occupation,
                ZipCode = zipCode
            };
        }
    }
}
