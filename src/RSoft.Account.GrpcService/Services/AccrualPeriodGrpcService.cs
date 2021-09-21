using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RSoft.Account.GrpcService.Extensions;
using RSoft.Account.Grpc.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace RSoft.Account.GrpcService.Services
{
    
    /// <summary>
    /// Accrual Period gRPC Service
    /// </summary>
    public class AccrualPeriodGrpcService : Grpc.Protobuf.AccrualPeriod.AccrualPeriodBase
    {

        #region Local Objects/Variables

        private readonly ILogger<AccrualPeriodGrpcService> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new Accrual Period gRPC Service
        /// </summary>
        /// <param name="logger">Logger object</param>
        public AccrualPeriodGrpcService(ILogger<AccrualPeriodGrpcService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Start an accrual period
        /// </summary>
        /// <param name="request">Accrual period request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<Empty> StartPeriod(PeriodRequest request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<Empty, StartAccrualPeriodCommand, bool>
            (
                nameof(StartPeriod),
                () => new() { Year = request.Year, Month = request.Month },
                (reply, result) => { },
                logger: _logger
            );

        /// <summary>
        /// Close an started accrual period
        /// </summary>
        /// <param name="request">Accrual period request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<Empty> ClosePeriod(PeriodRequest request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<Empty, CloseAccrualPeriodCommand, bool>
            (
                nameof(ClosePeriod),
                () => new(),
                (reply, result) => result.Response = true,
                logger: _logger
            );

        /// <summary>
        /// List accrual period
        /// </summary>
        /// <param name="request">Accrual period request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<ListPeriodReply> ListPeriod(Empty request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<ListPeriodReply, ListAccrualPeriodCommand, IEnumerable<AccrualPeriodDto>>
            (
                nameof(ListPeriod),
                () => new(),
                (reply, result) => reply.Data.Add(result.Response.Map()),
                logger: _logger
            );

        #endregion

    }
}
