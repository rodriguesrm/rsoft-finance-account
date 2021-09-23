using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.Contracts.FilterArguments
{

    /// <summary>
    /// Period year/month filter model
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class PeriodYearMonthFilter
    {

        #region Constructors

        /// <summary>
        /// Create a new filter object
        /// </summary>
        /// <param name="year">Year to filter</param>
        /// <param name="month">Month to filter</param>
        public PeriodYearMonthFilter(int year, int month)
        {
            Year = year;
            Month = month;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Year number (2020...)
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Month number (1..12)
        /// </summary>
        public int Month { get; set; }

        #endregion

    }
}
