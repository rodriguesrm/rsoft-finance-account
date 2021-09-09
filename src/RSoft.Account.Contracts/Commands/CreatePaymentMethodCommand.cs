using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Create PaymentMethod command contract 
    /// </summary>
    public class CreatePaymentMethodCommand : IRequest<CommandResult<Guid?>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paymentType">Payment type code (number)</param>
        public CreatePaymentMethodCommand(string name, int? paymentType)
        {
            Name = name;
            PaymentType = paymentType;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// PaymentMethod name description
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Payment type code (number)
        /// </summary>
        public int? PaymentType { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<Guid?> Response { get; set; }

        #endregion

    }
}
