using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using System;
using System.Threading.Tasks;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.GrpcService.Extensions;
using System.Collections.Generic;
using RSoft.Entry.Grpc.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace RSoft.Entry.GrpcService.Services
{

    /// <summary>
    /// Entry gRPC Service
    /// </summary>
    [Authorize]
    public class EntryGrpcService : Grpc.Protobuf.Entry.EntryBase
    {

        #region Local objects/variables

        private readonly ILogger<EntryGrpcService> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new Entry gRPC Service
        /// </summary>
        /// <param name="logger">Logger object</param>
        public EntryGrpcService(ILogger<EntryGrpcService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Create a new entry
        /// </summary>
        /// <param name="request">Entry request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<CreateEntryReply> CreateEntry(CreateEntryRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<CreateEntryReply, CreateEntryCommand, Guid?>
            (
                nameof(CreateEntry),
                () =>
                {
                    Guid? categoryId = null;
                    if (Guid.TryParse(request.CategoryId, out Guid id))
                        categoryId = id;
                    return new CreateEntryCommand(request.Name, categoryId);
                },
                (reply, result) => reply.Id = result.Response.Value.ToString(),
                logger: _logger
            );

        /// <summary>
        /// Update an existing entry
        /// </summary>
        /// <param name="request">Entry request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<Empty> UpdateEntry(UpdateEntryRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<Empty, UpdateEntryCommand, bool>
            (
                nameof(UpdateEntry),
                () =>
                {
                    Guid? categoryId = null;
                    if (Guid.TryParse(request.CategoryId, out Guid id))
                        categoryId = id;
                    return new UpdateEntryCommand(new Guid(request.Id), request.Name, categoryId);
                },
                logger: _logger
            );

        /// <summary>
        /// Enable an entry
        /// </summary>
        /// <param name="request">Entry request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<Empty> EnableEntry(ChangeStatusEntryRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<Empty, ChangeStatusEntryCommand, bool>
            (
                nameof(DisableEntry),
                () => new(new Guid(request.Id), true),
                logger: _logger
            );

        /// <summary>
        /// Disable an entry
        /// </summary>
        /// <param name="request">Entry request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<Empty> DisableEntry(ChangeStatusEntryRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<Empty, ChangeStatusEntryCommand, bool>
            (
                nameof(DisableEntry),
                () => new(new Guid(request.Id), false),
                logger: _logger
            );

        /// <summary>
        /// Get an entry by id
        /// </summary>
        /// <param name="request">Entry request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<EntryDetail> GetEntry(GetEntryRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<EntryDetail, GetEntryByIdCommand, EntryDto>
            (
                nameof(GetEntry),
                () => new(new Guid(request.Id)),
                (reply, result) => result.Response.Map(reply),
                logger: _logger
            );

        /// <summary>
        /// List all entries
        /// </summary>
        /// <param name="request">Entry request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<ListEntryReply> ListEntry(Empty request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<ListEntryReply, ListEntryCommand, IEnumerable<EntryDto>>
            (
                nameof(ListEntry),
                () => new(),
                (reply, result) => reply.Data.Add(result.Response.Map()),
                logger: _logger
            );

        #endregion

    }
}
