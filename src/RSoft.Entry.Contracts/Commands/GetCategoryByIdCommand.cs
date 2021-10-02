using MediatR;
using RSoft.Entry.Contracts.Models;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Update category command contract 
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class GetCategoryByIdCommand : IRequest<CommandResult<CategoryDto>>
    {

        #region Constructors

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id">Category id</param>
        public GetCategoryByIdCommand(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Category id
        /// </summary>
        public Guid Id { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<CategoryDto> Response { get; set; }

        #endregion

    }
}
