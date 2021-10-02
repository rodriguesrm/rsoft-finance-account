using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Create category command contract 
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class CreateCategoryCommand : IRequest<CommandResult<Guid?>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="name"></param>
        public CreateCategoryCommand(string name)
        {
            Name = name;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Category name description
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<Guid?> Response { get; set; }

        #endregion

    }
}
