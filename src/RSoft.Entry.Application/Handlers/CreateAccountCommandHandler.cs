﻿using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Ports;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Create account command handler
    /// </summary>
    public class CreateAccountCommandHandler : CreateCommandHandlerBase<CreateAccountCommand, Guid?, DomainEntry>, IRequestHandler<CreateAccountCommand, CommandResult<Guid?>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IEntryDomainService _accountDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a command handler object instance
        /// </summary>
        /// <param name="accountDomainService">Account domain service object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="bus">Message bus control</param>
        public CreateAccountCommandHandler(IUnitOfWork uow, IEntryDomainService accountDomainService, ILogger<CreateAccountCommandHandler> logger, IBusControl bus) : base(logger)
        {
            _uow = uow;
            _accountDomainService = accountDomainService;
            _bus = bus;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<Guid?>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override DomainEntry PrepareEntity(CreateAccountCommand request)
        {
            DomainEntry entity = new();
            entity.Name = request.Name;
            if (request.CategoryId.HasValue)
                entity.Category = new(request.CategoryId.Value);
            return entity;
        }

        ///<inheritdoc/>
        protected override async Task<Guid?> SaveAsync(DomainEntry entity, CancellationToken cancellationToken)
        {
            AccountCreatedEvent accountCreatedEvent = new(entity.Id, entity.Name, entity.Category.Id);
            entity = await _accountDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            await _bus.Publish(accountCreatedEvent, cancellationToken);
            return entity.Id;
        }

        #endregion

    }
}
