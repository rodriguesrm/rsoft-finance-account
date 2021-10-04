using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Stubs
{

    public class MediatorSub : IMediator
    {

        #region Local objects/variables

        private static object _mockResponse = null;

        #endregion

        #region Static Methods

        /// <summary>
        /// Set mock response for test
        /// </summary>
        /// <param name="response">Object instance to mock</param>
        public static void SetMockResponse(object response)
        {
            _mockResponse = response;
        }

        #endregion

        #region Public methods

        public Task Publish(object notification, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            => Task.CompletedTask;

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            TResponse response = Activator.CreateInstance<TResponse>();
            if (_mockResponse != null)
            {
                if (_mockResponse is Exception)
                    throw (Exception)_mockResponse;

                if (response.GetType() == _mockResponse.GetType())
                {
                    response = (TResponse)_mockResponse;
                    _mockResponse = null;
                }
            }
            return Task.FromResult(response);
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            object response = default;
            return Task.FromResult(response);
        }

        #endregion

    }

}
