using MediatR;
using RSoft.Account.Contracts.Models;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Update AccrualPeriod command contract 
    /// </summary>
    public class ListAccrualPeriodCommand : IRequest<CommandResult<IEnumerable<AccrualPeriodDto>>>
    {

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<IEnumerable<AccrualPeriodDto>> Response { get; set; }

        #endregion

    }
}
