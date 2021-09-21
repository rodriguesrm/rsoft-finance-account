using MediatR;
using RSoft.Lib.Design.Application.Commands;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Start Accrual Period command contract 
    /// </summary>
    public class StartAccrualPeriodCommand : IRequest<CommandResult<bool>>
    {

        #region Constructors

        /// <summary>
        /// Create command instance
        /// </summary>
        public StartAccrualPeriodCommand() { }

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
