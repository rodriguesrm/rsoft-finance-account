﻿using MediatR;
using RSoft.Entry.Contracts.Models;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Update Account command contract 
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
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
