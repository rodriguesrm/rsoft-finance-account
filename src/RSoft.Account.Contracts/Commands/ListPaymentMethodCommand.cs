using MediatR;
using RSoft.Account.Contracts.Models;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Update PaymentMethod command contract 
    /// </summary>
    public class ListPaymentMethodCommand : IRequest<CommandResult<IEnumerable<PaymentMethodDto>>>
    {

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<IEnumerable<PaymentMethodDto>> Response { get; set; }

        #endregion

    }
}
