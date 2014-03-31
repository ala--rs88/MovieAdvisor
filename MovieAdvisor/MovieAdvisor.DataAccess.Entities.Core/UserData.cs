namespace MovieAdvisor.DataAccess.Entities.Core
{
    public class UserData
    {
        public int UserId { get; set; }

        public GenderEnumData Gender { get; set; }

        public AgeRangeEnumData AgeRange { get; set; }

        public UserOccupationEnumData Occupation { get; set; }

        public string ZipCode { get; set; }
    }
}
