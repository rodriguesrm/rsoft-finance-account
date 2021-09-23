using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.Contracts.Commands
{

    /// <summary>
    /// Create account command contract 
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Anemic class")]
    public class CreateAccountCommand : IRequest<CommandResult<Guid?>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="name">Account name</param>
        /// <param name="categoryId">Category id</param>
        public CreateAccountCommand(string name, Guid? categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Account name description
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
