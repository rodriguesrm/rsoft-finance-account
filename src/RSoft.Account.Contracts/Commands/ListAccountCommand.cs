using MediatR;
using RSoft.Account.Contracts.Models;
using RSoft.Finance.Contracts.Commands;
using System.Collections.Generic;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Update Account command contract 
    /// </summary>
    public class ListAccountCommand : IRequest<CommandResult<IEnumerable<AccountDto>>>
    {

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<IEnumerable<AccountDto>> Response { get; set; }

        #endregion

    }
}
