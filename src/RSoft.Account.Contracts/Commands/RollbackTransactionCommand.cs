using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Reserve Transaction command contract 
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class RollbackTransactionCommand : IRequest<CommandResult<Guid?>>
    {

        #region Constructors

        /// <summary>
        /// Create command instance
        /// </summary>
        /// <param name="transactionId">Transaction ID to be rolled back</param>
        /// <param name="comment">Transaction Comments/Annotations</param>
        public RollbackTransactionCommand(Guid transactionId, string comment)
        {
            Comment = comment;
            TransactionId = transactionId;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Transaction ID to be rolled back
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Transaction Comments/Annotations
        /// </summary>
        public string Comment { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<Guid?> Response { get; set; }

        #endregion

    }
}
