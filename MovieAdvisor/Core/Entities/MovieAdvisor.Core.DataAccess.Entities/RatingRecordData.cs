namespace MovieAdvisor.Core.DataAccess.Entities
{
    class RatingRecordData
    {
        public int UserId { get; set; }

        public int MovieId { get; set; }

        public RatingValueEnumData Value { get; set; }

        /// <summary>
        /// Gets or sets timestamp.
        /// Timestamp is represented in seconds since the epoch as returned by time(2).
        /// </summary>
        public int Timestamp { get; set; }
    }
}
