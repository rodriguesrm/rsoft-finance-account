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

        /// <summary>
        /// Create command instance
        /// </summary>
        /// <param name="date">Transaction date</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="amount">Transaction amount</param>
        /// <param name="comment">Transaction Comments/Annotations</param>
        /// <param name="accountId">Account id</param>
        /// <param name="paymentMethodId">Payment method id</param>
        public CreateTransactionCommand(DateTime date, int transactionType, float amount, string comment, Guid? accountId, Guid? paymentMethodId)
        {
            Date = date;
            TransactionType = transactionType;
            Amount = amount;
            Comment = comment;
            AccountId = accountId;
            PaymentMethodId = paymentMethodId;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Transaction type
        /// </summary>
        public int TransactionType { get; set; }

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
