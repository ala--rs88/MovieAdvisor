namespace MovieAdvisor.Common.Unity
{
    public enum UnityRegistryApplicationOrderEnum
    {
        /// <summary>
        /// The should be applied last or order is not specified.
        /// </summary>
        ShouldBeAppliedLastOrOrderIsNotSpecified = 0,

        /// <summary>
        /// The should be applied in first stage.
        /// </summary>
        ShouldBeAppliedInFirstStage = 1,

        /// <summary>
        /// The should be applied in second stage.
        /// </summary>
        ShouldBeAppliedInSecondStage = 2
    }
}
