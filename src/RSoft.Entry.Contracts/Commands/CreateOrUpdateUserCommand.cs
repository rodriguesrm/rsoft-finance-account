using MediatR;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Contracts.Commands
{

    /// <summary>
    /// Create User command contract 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateOrUpdateUserCommand : IRequest<CommandResult<Guid?>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="isCreatedCommand">Command type flag</param>
        /// <param name="id">User id key value</param>
        /// <param name="firstName">User first name</param>
        /// <param name="lastName">User last name</param>
        /// <param name="isActive">Active status flag</param>
        public CreateOrUpdateUserCommand(bool isCreatedCommand, Guid id, string firstName, string lastName, bool isActive)
        {
            IsCreatedCommand = isCreatedCommand;
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IsActive = isActive;
        }

        #endregion

        #region Request Data

        /// <summary>
        /// Command type flag
        /// </summary>
        public bool IsCreatedCommand { get; private set; }

        /// <summary>
        /// User id key value
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Active status flag
        /// </summary>
        public bool IsActive { get; private set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<Guid?> Response { get; set; }

        #endregion

    }
}
