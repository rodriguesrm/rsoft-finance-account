using MediatR;
using RSoft.Entry.Contracts.Models;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Update PaymentMethod command contract 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetPaymentMethodByIdCommand : IRequest<CommandResult<PaymentMethodDto>>
    {

        #region Constructors

        /// <summary>
        /// Get PaymentMethod by id
        /// </summary>
        /// <param name="id">PaymentMethod id</param>
        public GetPaymentMethodByIdCommand(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// PaymentMethod id
        /// </summary>
        public Guid Id { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<PaymentMethodDto> Response { get; set; }

        #endregion

    }
}
