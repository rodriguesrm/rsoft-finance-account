using GreenPipes;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Stubs
{
    public class ConsumeContextStub<T> : ConsumeContext<T>
        where T : class
    {

        public T Message { get; set; }
        public ReceiveContext ReceiveContext { get; set; }
        public Task ConsumeCompleted => Task.CompletedTask;
        public IEnumerable<string> SupportedMessageTypes { get; set; }
        public CancellationToken CancellationToken { get; set; }

        private readonly Guid? _messageId = Guid.NewGuid();
        public Guid? MessageId => _messageId;

        private readonly Guid? _requestId = Guid.NewGuid();
        public Guid? RequestId => _requestId;
        
        private readonly Guid? _correlationId = Guid.NewGuid();
        public Guid? CorrelationId => _correlationId;
        
        private readonly Guid? _conversationId = Guid.NewGuid();
        public Guid? ConversationId => _conversationId;
        
        private readonly Guid? _initiatorId = Guid.NewGuid();
        public Guid? InitiatorId => _initiatorId;

        private readonly DateTime? _expirationTime = DateTime.UtcNow.AddMinutes(10);
        public DateTime? ExpirationTime => _expirationTime;

        public Uri SourceAddress { get; set; }
        public Uri DestinationAddress { get; set; }
        public Uri ResponseAddress { get; set; }
        public Uri FaultAddress { get; set; }
        
        private readonly DateTime? _sentTime = DateTime.UtcNow.AddSeconds(-10);
        public DateTime? SentTime => _sentTime;
        
        public Headers Headers { get; set; }
        public HostInfo Host { get; set; }

        public void AddConsumeTask(Task task)
        {
            throw new NotImplementedException();
        }

        public T1 AddOrUpdatePayload<T1>(PayloadFactory<T1> addFactory, UpdatePayloadFactory<T1> updateFactory) where T1 : class
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
        {
            throw new NotImplementedException();
        }

        public ConnectHandle ConnectSendObserver(ISendObserver observer)
        {
            throw new NotImplementedException();
        }

        public T1 GetOrAddPayload<T1>(PayloadFactory<T1> payloadFactory) where T1 : class
        {
            throw new NotImplementedException();
        }

        public Task<ISendEndpoint> GetSendEndpoint(Uri address)
        {
            throw new NotImplementedException();
        }

        public bool HasMessageType(Type messageType)
        {
            throw new NotImplementedException();
        }

        public bool HasPayloadType(Type payloadType)
        {
            throw new NotImplementedException();
        }

        public Task NotifyConsumed(TimeSpan duration, string consumerType)
        {
            throw new NotImplementedException();
        }

        public Task NotifyConsumed<T1>(ConsumeContext<T1> context, TimeSpan duration, string consumerType) where T1 : class
        {
            throw new NotImplementedException();
        }

        public Task NotifyFaulted(TimeSpan duration, string consumerType, Exception exception)
        {
            throw new NotImplementedException();
        }

        public Task NotifyFaulted<T1>(ConsumeContext<T1> context, TimeSpan duration, string consumerType, Exception exception) where T1 : class
        {
            throw new NotImplementedException();
        }

        public Task Publish<T1>(T1 message, CancellationToken cancellationToken = default) where T1 : class
            => Task.CompletedTask;

        public Task Publish<T1>(T1 message, IPipe<PublishContext<T1>> publishPipe, CancellationToken cancellationToken = default) where T1 : class
            => Task.CompletedTask;

        public Task Publish<T1>(T1 message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T1 : class
            => Task.CompletedTask;

        public Task Publish(object message, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task Publish(object message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task Publish(object message, Type messageType, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task Publish<T1>(object values, CancellationToken cancellationToken = default) where T1 : class
            => Task.CompletedTask;

        public Task Publish<T1>(object values, IPipe<PublishContext<T1>> publishPipe, CancellationToken cancellationToken = default) where T1 : class
        {
            T1 result = Activator.CreateInstance<T1>();
            return Task.FromResult(result);
        }

        public Task Publish<T1>(object values, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T1 : class
        {
            T1 result = Activator.CreateInstance<T1>();
            return Task.FromResult(result);
        }

        public void Respond<T1>(T1 message) where T1 : class
        {
            throw new NotImplementedException();
        }

        public Task RespondAsync<T1>(T1 message) where T1 : class
            => Task.CompletedTask;

        public Task RespondAsync<T1>(T1 message, IPipe<SendContext<T1>> sendPipe) where T1 : class
        {
            T1 result = Activator.CreateInstance<T1>();
            return Task.FromResult(result);
        }

        public Task RespondAsync<T1>(T1 message, IPipe<SendContext> sendPipe) where T1 : class
        {
            T1 result = Activator.CreateInstance<T1>();
            return Task.FromResult(result);
        }

        public Task RespondAsync(object message)
            => Task.CompletedTask;

        public Task RespondAsync(object message, Type messageType)
            => Task.CompletedTask;

        public Task RespondAsync(object message, IPipe<SendContext> sendPipe)
            => Task.CompletedTask;

        public Task RespondAsync(object message, Type messageType, IPipe<SendContext> sendPipe)
            => Task.CompletedTask;

        public Task RespondAsync<T1>(object values) where T1 : class
        {
            T1 result = Activator.CreateInstance<T1>();
            return Task.FromResult(result);
        }

        public Task RespondAsync<T1>(object values, IPipe<SendContext<T1>> sendPipe) where T1 : class
        {
            throw new NotImplementedException();
        }

        public Task RespondAsync<T1>(object values, IPipe<SendContext> sendPipe) where T1 : class
        {
            T1 result = Activator.CreateInstance<T1>();
            return Task.FromResult(result);
        }

        public bool TryGetMessage<T1>(out ConsumeContext<T1> consumeContext) where T1 : class
        {
            throw new NotImplementedException();
        }

        public bool TryGetPayload<T1>(out T1 payload) where T1 : class
        {
            throw new NotImplementedException();
        }
    }
}
