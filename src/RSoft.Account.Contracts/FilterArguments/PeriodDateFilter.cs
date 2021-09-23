using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.Contracts.FilterArguments
{

    /// <summary>
    /// Period date filter model
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
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
