namespace MovieAdvisor.Core.Business.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public GenderEnum Gender { get; set; }

        public AgeRangeEnum AgeRange { get; set; }

        public UserOccupationEnum Occupation { get; set; }

        public string ZipCode { get; set; }
    }
}
