using MediatR;
using RSoft.Account.Contracts.Models;
using RSoft.Finance.Contracts.Commands;
using System;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Update category command contract 
    /// </summary>
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
