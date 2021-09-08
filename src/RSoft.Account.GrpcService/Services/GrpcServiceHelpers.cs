using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Grpc.Core;

namespace RSoft.Account.GrpcService.Services
{

    /// <summary>
    /// Provides helpers for gRPC Services
    /// </summary>
    internal class GrpcServiceHelpers
    {

        /// <summary>
        /// Send command via mediator
        /// </summary>
        /// <typeparam name="TReply"></typeparam>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="createCommand"></param>
        /// <param name="successAction"></param>
        internal static async Task<TReply> SendCommand<TReply, TCommand, TResult>
        (
            string methodName,
            Func<TCommand> createCommand,
            Action<TReply, CommandResult<TResult>> successAction = null,
            ILogger logger = null,
            IMediator mediator = null
        )
            where TReply : class
            where TCommand : IRequest<CommandResult<TResult>>
        {

            mediator ??= ServiceActivator.GetScope().ServiceProvider.GetService<IMediator>();
            logger ??= ServiceActivator.GetScope().ServiceProvider.GetService<ILogger<GrpcServiceHelpers>>();

            logger.LogInformation($"Starting {methodName}");
            TReply reply = Activator.CreateInstance<TReply>();
            TCommand command = createCommand();
            CommandResult<TResult> result = await mediator.Send(command);
            if (result.Success)
            {
                successAction?.Invoke(reply, result);
            }
            else
            {
                string jsonNotifications = JsonSerializer.Serialize(result.Errors);
                Status rpcStatus = new(StatusCode.InvalidArgument, jsonNotifications);
                throw new RpcException(status: rpcStatus, message: "BadRequest");
            }
            logger.LogInformation($"{methodName} finished {(result.Success ? "sucessful" : "with errors")}");
            return reply;
        }

    }
}
