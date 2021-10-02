using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Close Accrual Period command contract 
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class CloseAccrualPeriodCommand : IRequest<CommandResult<bool>>
    {

        #region Constructors

        /// <summary>
        /// Create command instance
        /// </summary>
        public CloseAccrualPeriodCommand() { }

        #endregion

        #region Request Data

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<bool> Response { get; set; }

        #endregion

    }
}
