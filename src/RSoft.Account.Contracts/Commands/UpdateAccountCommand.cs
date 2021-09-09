using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Update Account command contract 
    /// </summary>
    public class UpdateAccountCommand : IRequest<CommandResult<bool>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="id">Account id</param>
        /// <param name="name">Account name</param>
        /// <param name="categoryId">Category id</param>
        public UpdateAccountCommand(Guid id, string name, Guid? categoryId)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Account id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Account name description
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
