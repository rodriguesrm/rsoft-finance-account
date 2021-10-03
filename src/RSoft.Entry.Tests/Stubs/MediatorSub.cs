using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Stubs
{

    public class MediatorSub : IMediator
    {
        public Task Publish(object notification, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            => Task.CompletedTask;

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            TResponse response = Activator.CreateInstance<TResponse>();
            return Task.FromResult(response);
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            object response = default;
            return Task.FromResult(response);
        }

    }

}
