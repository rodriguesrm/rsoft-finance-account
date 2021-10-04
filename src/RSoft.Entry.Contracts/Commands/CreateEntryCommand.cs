using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Create entry command contract 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateEntryCommand : IRequest<CommandResult<Guid?>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <param name="categoryId">Category id</param>
        public CreateEntryCommand(string name, Guid? categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Entry name description
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category id
        /// </summary>
        public Guid? CategoryId { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<Guid?> Response { get; set; }

        #endregion

    }
}
