using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
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
        /// <typeparam name="TReply">Type of reply</typeparam>
        /// <typeparam name="TCommand">Type of command</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="methodName">Method name for logger</param>
        /// <param name="createCommand">Action to create command</param>
        /// <param name="successAction">Action to perfom when get success from command</param>
        /// <param name="logger">Logger object</param>
        /// <param name="mediator">Command mediator</param>
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
