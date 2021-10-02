using MediatR;
using RSoft.Entry.Contracts.Models;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Update Transaction command contract 
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class GetTransactionByIdCommand : IRequest<CommandResult<TransactionDto>>
    {

        #region Constructors

        /// <summary>
        /// Get Transaction by id
        /// </summary>
        /// <param name="id">Transaction id</param>
        public GetTransactionByIdCommand(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Transaction id
        /// </summary>
        public Guid Id { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<TransactionDto> Response { get; set; }

        #endregion

    }
}
