using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.FilterArguments
{

    /// <summary>
    /// Period date filter model
    /// </summary>
    [ExcludeFromCodeCoverage]
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
