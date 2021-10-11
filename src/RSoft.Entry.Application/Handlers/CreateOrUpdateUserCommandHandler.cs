using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Create or Update User command handler
    /// </summary>
    public class CreateOrUpdateUserCommandHandler : 
        CreateOrUpdateCommandHandlerBase<CreateOrUpdateUserCommand, Guid?, User>, IRequestHandler<CreateOrUpdateUserCommand, CommandResult<Guid?>>
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
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        public CreateOrUpdateUserCommandHandler(IUserDomainService userDomainService, ILogger<CreateOrUpdateUserCommandHandler> logger, IUnitOfWork uow) : base(logger)
        {
            _userDomainService = userDomainService;
            _uow = uow;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<User> GetEntityByKeyAsync(CreateOrUpdateUserCommand request, CancellationToken cancellationToken)
            => await _userDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override User PrepareEntity(CreateOrUpdateUserCommand request, User entity, bool isUpdate)
        {

            Name name = new(request.FirstName, request.LastName);
            bool isActive = request.IsActive;

            if (isUpdate)
            {
                if (request.IsCreatedCommand)
                {
                    name = entity.Name;
                    isActive = entity.IsActive;
                }
            }
            else
            {
                entity = new User(request.Id);
            }
            entity.Name = name;
            entity.IsActive = isActive;
            return entity;
        }

        ///<inheritdoc/>
        protected override async Task<Guid?> SaveAsync(User entity, bool isUpdate, CancellationToken cancellationToken)
        {
            if (isUpdate)
                _ = _userDomainService.Update(entity.Id, entity);
            else
                _ = await _userDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        #endregion

        #region Handlers

        ///<inheritdoc/>
        public async Task<CommandResult<Guid?>> Handle(CreateOrUpdateUserCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
