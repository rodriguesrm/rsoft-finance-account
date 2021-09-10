using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Create Transaction command contract 
    /// </summary>
    public class CreateTransactionCommand : IRequest<CommandResult<Guid?>>
    {

        #region Constructors



        #endregion

        #region Request Data

        /// <summary>
        /// Transaction year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Transaction month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Transaction Comments/Annotations
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Account data
        /// </summary>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Payment method data
        /// </summary>
        public Guid? PaymentMethodId { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<Guid?> Response { get; set; }

        #endregion

    }
}
