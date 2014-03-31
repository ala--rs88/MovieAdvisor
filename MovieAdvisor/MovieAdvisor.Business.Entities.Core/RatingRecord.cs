namespace MovieAdvisor.Business.Entities.Core
{
    public class RatingRecord
    {
        public int UserId { get; set; }

        public int MovieId { get; set; }

        public RatingValueEnum Value { get; set; }

        /// <summary>
        /// Gets or sets timestamp.
        /// Timestamp is represented in seconds since the epoch as returned by time(2).
        /// </summary>
        public int Timestamp { get; set; }
    }
}
