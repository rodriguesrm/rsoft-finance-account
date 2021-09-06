using MediatR;
using RSoft.Finance.Contracts.Commands;
using System;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Update category command contract 
    /// </summary>
    public class UpdateCategoryCommand : IRequest<CommandResult<bool>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="name">Category name</param>
        public UpdateCategoryCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Category id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Category name description
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<bool> Response { get; set; }

        #endregion

    }
}
