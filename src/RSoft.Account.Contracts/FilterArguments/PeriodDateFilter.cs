using System;

namespace RSoft.Account.Contracts.FilterArguments
{

    /// <summary>
    /// Period date filter model
    /// </summary>
    public class PeriodDateFilter
    {

        #region Properties

        /// <summary>
        /// Start date
        /// </summary>
        public DateTime? StartAt { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        public DateTime? EndAt { get; set; }

        #endregion

    }
}
