using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Register Start Accrual Period command contract 
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class RegisterStartAccrualPeriodCommand : IRequest<CommandResult<bool>>
    {

        #region Constructors

        /// <summary>
        /// Create command instance
        /// </summary>
        public RegisterStartAccrualPeriodCommand() { }

        #endregion

        #region Request Data

        /// <summary>
        /// Accrual period year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Accrual period month
        /// </summary>
        public int Month { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<bool> Response { get; set; }

        #endregion

    }
}
