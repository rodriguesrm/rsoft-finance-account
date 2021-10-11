using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Lib.Design.Application.Handlers;
using MassTransit;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Common.ValueObjects;
using System;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Create User command handler
    /// </summary>
    public class DeleteUserCommandHandler : UpdateCommandHandlerBase<DeleteUserCommand, bool, User>, IRequestHandler<DeleteUserCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IUserDomainService _userDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="userDomainService">User domain service object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="logger">Logger object</param>
        public DeleteUserCommandHandler(IUserDomainService userDomainService, IUnitOfWork uow, ILogger<DeleteUserCommandHandler> logger) : base(logger)
        {
            _userDomainService = userDomainService;
            _uow = uow;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<User> GetEntityByKeyAsync(DeleteUserCommand request, CancellationToken cancellationToken)
            => await _userDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(DeleteUserCommand request, User entity)
        {
            entity.Name = new Name("USER", "DELETED");
            entity.IsActive = false;
        }

        ///<inheritdoc/>
        protected override async Task<bool> SaveAsync(User entity, CancellationToken cancellationToken)
        {
            _ = _userDomainService.Update(entity.Id, entity);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
