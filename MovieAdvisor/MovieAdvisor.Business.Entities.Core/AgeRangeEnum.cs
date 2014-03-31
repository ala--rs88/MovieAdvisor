namespace MovieAdvisor.Business.Entities.Core
{
    public enum AgeRangeEnum
    {
        /// <summary>
        /// The age unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Age from 0 up to 18.
        /// </summary>
        Under18 = 1,

        /// <summary>
        /// Age from 18 up to 24.
        /// </summary>
        From18To24 = 2,

        /// <summary>
        /// Age from 25 up to 34.
        /// </summary>
        From25To34 = 3,

        /// <summary>
        /// Age from 35 up to 44.
        /// </summary>
        From35To44 = 4,

        /// <summary>
        /// Age from 45 up to 49.
        /// </summary>
        From45To49 = 5,

        /// <summary>
        /// Age from 50 up to 55.
        /// </summary>
        From50To55 = 6,

        /// <summary>
        /// Age 56 and older.
        /// </summary>
        OlderThan55 = 7
    }
}
