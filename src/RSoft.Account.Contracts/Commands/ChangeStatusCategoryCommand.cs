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
    public class ChangeStatusCategoryCommand : IRequest<CommandResult<bool>>
    {

        #region Constructors

        /// <summary>
        /// Create a new command instance
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="isActive">Active status flag</param>
        public ChangeStatusCategoryCommand(Guid id, bool isActive)
        {
            Id = id;
            IsActive = isActive;
        }

        #endregion

        #region Request Data


        /// <summary>
        /// Category id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Active status flag
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Result Data

        /// <summary>
        /// Response data 
        /// </summary>
        public CommandResult<bool> Response { get; set; }

        #endregion

    }
}
