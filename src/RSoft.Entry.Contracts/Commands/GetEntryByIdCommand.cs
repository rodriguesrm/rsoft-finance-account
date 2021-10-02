using MediatR;
using RSoft.Entry.Contracts.Models;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Update Entry command contract 
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class GetEntryByIdCommand : IRequest<CommandResult<EntryDto>>
    {

        #region Constructors

        /// <summary>
        /// Get Entry by id
        /// </summary>
        /// <param name="id">Entry id</param>
        public GetEntryByIdCommand(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Entry id
        /// </summary>
        public Guid Id { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<EntryDto> Response { get; set; }

        #endregion

    }
}
