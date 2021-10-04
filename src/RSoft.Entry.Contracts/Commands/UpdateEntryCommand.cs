using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Update Entry command contract 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UpdateEntryCommand : IRequest<CommandResult<bool>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="id">Entry id</param>
        /// <param name="name">Entry name</param>
        /// <param name="categoryId">Category id</param>
        public UpdateEntryCommand(Guid id, string name, Guid? categoryId)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Entry id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Entry name description
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category id value
        /// </summary>
        public Guid? CategoryId { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<bool> Response { get; set; }

        #endregion

    }
}
