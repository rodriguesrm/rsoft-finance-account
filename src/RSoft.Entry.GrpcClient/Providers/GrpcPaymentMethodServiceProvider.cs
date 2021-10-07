using Grpc.Core;
using Microsoft.Extensions.Logging;
using RSoft.Entry.GrpcClient.Abstractions;
using RSoft.Entry.GrpcClient.Extensions;
using RSoft.Entry.GrpcClient.Models;
using System;
using System.Threading.Tasks;
using RSoft.Entry.Grpc.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace RSoft.Entry.GrpcClient.Providers
{

    /// <summary>
    /// RSoft gRpc PaymentMethod Service provider
    /// </summary>
    internal class GrpcPaymentMethodServiceProvider : IGrpcPaymentMethodServiceProvider
    {

        #region Local objects/variables

        private readonly ILogger<GrpcPaymentMethodServiceProvider> _logger;
        private readonly IGrpcChannelFactory _channelFactory;

        private PaymentMethod.PaymentMethodClient _paymentMethodClient = null;
        private string _token = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creat a new Grpc Provider instance
        /// </summary>
        /// <param name="channelFactory">gRPC Channel factory object instance</param>
        /// <param name="loggerFactory">Logger factory object</param>
        public GrpcPaymentMethodServiceProvider
        (
            IGrpcChannelFactory channelFactory,
            ILoggerFactory loggerFactory
        )
        {
            _channelFactory = channelFactory;
            _logger = loggerFactory?.CreateLogger<GrpcPaymentMethodServiceProvider>();
        }

        #endregion

        #region Local methods

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public void SetToken(string token)
        {
            _token = token;
            _paymentMethodClient = new PaymentMethod.PaymentMethodClient(_channelFactory.CreateChannel(_token));
        }

        ///<inheritdoc/>
        public async Task<CreatePaymentMethodResponse> CreatePaymentMethod(string name, int paymentType)
        {

            CreatePaymentMethodResponse resp;
            CreatePaymentMethodRequest request =
                new CreatePaymentMethodRequest() { Name = name, PaymentType = paymentType };

            _logger?.LogInformation("Call CreatePaymentMethod on gRPC Service on {urlService}", _channelFactory.UrlServer);
            try
            {
                CreatePaymentMethodReply reply = await _paymentMethodClient.CreatePaymentMethodAsync(request);
                resp = reply.ToCreatePaymentMethodResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToCreatePaymentMethodResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToCreatePaymentMethodResponse();
            }

            _logger?.LogInformation("CreatePaymentMethod Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<UpdatePaymentMethodResponse> UpdatePaymentMethod(Guid id, string name, int paymentType)
        {

            UpdatePaymentMethodResponse resp;
            UpdatePaymentMethodRequest request =
                new UpdatePaymentMethodRequest() { Id = id.ToString(), Name = name, PaymentType = paymentType };

            _logger?.LogInformation("Call UpdatePaymentMethod on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                _ = await _paymentMethodClient.UpdatePaymentMethodAsync(request);
                resp = new UpdatePaymentMethodResponse(StatusCode.OK);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToUpdatePaymentMethodResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToUpdatePaymentMethodResponse();
            }

            _logger?.LogInformation("UpdatePaymentMethod Finished");

            return resp;
        }

        ///<inheritdoc/>
        public async Task<ChangePaymentMethodStatusResponse> EnablePaymentMethod(Guid id)
        {

            ChangePaymentMethodStatusResponse resp;
            ChangeStatusPaymentMethodRequest request = new ChangeStatusPaymentMethodRequest() { Id = id.ToString() };

            _logger?.LogInformation("Call EnablePaymentMethod on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                _ = await _paymentMethodClient.EnablePaymentMethodAsync(request);
                resp = new ChangePaymentMethodStatusResponse(StatusCode.OK);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToChangePaymentMethodStatusResponse(nameof(EnablePaymentMethod));
            }
            catch (Exception ex)
            {
                resp = ex.ToChangePaymentMethodStatusResponse();
            }

            _logger?.LogInformation("EnablePaymentMethod Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<ChangePaymentMethodStatusResponse> DisablePaymentMethod(Guid id)
        {

            ChangePaymentMethodStatusResponse resp;
            ChangeStatusPaymentMethodRequest request = new ChangeStatusPaymentMethodRequest() { Id = id.ToString() };

            _logger?.LogInformation("Call EnablePaymentMethod on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                _ = await _paymentMethodClient.DisablePaymentMethodAsync(request);
                resp = new ChangePaymentMethodStatusResponse(StatusCode.OK);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToChangePaymentMethodStatusResponse(nameof(DisablePaymentMethod));
            }
            catch (Exception ex)
            {
                resp = ex.ToChangePaymentMethodStatusResponse();
            }

            _logger?.LogInformation("DisablePaymentMethod Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<PaymentMethodDetailResponse> GetPaymentMethod(Guid id)
        {
            GetPaymentMethodRequest request = new GetPaymentMethodRequest() { Id = id.ToString() };
            PaymentMethodDetailResponse resp;

            _logger?.LogInformation("Call GetPaymentMethod on gRPC Service on {urlService}", _channelFactory.UrlServer);


            try
            {
                PaymentMethodDetail reply = await _paymentMethodClient.GetPaymentMethodAsync(request);
                resp = reply.ToPaymentMethodDetailResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToPaymentMethodDetailResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToPaymentMethodDetailResponse();
            }

            _logger?.LogInformation("GetPaymentMethod Finished");

            return resp;
        }

        ///<inheritdoc/>
        public async Task<ListPaymentMethodDetailResponse> ListPaymentMethod()
        {

            Empty request = new Empty();
            ListPaymentMethodDetailResponse resp;

            _logger?.LogInformation("Call ListPaymentMethod on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                ListPaymentMethodReply reply = await _paymentMethodClient.ListPaymentMethodAsync(request);
                resp = reply.ToListPaymentMethodDetailResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToListPaymentMethodDetailResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToListPaymentMethodDetailResponse();
            }

            _logger?.LogInformation("ListPaymentMethod Finished");

            return resp;
        }

        #endregion

    }
}