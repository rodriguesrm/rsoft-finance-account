using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Create Transaction command contract 
    /// </summary>
    [ExcludeFromCodeCoverage]
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
        /// <param name="entryId">Entry id</param>
        /// <param name="paymentMethodId">Payment method id</param>
        public CreateTransactionCommand(DateTime date, int transactionType, float amount, string comment, Guid? entryId, Guid? paymentMethodId)
        {
            Date = date;
            TransactionType = transactionType;
            Amount = amount;
            Comment = comment;
            EntryId = entryId;
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
        /// Entry data
        /// </summary>
        public Guid? EntryId { get; set; }

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
