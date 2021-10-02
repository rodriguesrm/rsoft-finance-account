using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Update PaymentMethod command contract 
    /// </summary>

    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class UpdatePaymentMethodCommand : IRequest<CommandResult<bool>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="id">PaymentMethod id</param>
        /// <param name="name">PaymentMethod name</param>
        /// <param name="paymentType">Payment type code (number)</param>
        public UpdatePaymentMethodCommand(Guid id, string name, int? paymentType)
        {
            Id = id;
            Name = name;
            PaymentType = paymentType;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// PaymentMethod id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// PaymentMethod name description
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Payment type
        /// </summary>
        public int? PaymentType { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<bool> Response { get; set; }

        #endregion

    }
}
