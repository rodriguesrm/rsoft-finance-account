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

    internal class GrpcAccrualPeriodServiceProvider : IGrpcAccrualPeriodServiceProvider
    {

        #region Local objects/variables

        private readonly ILogger<GrpcAccrualPeriodServiceProvider> _logger;
        private readonly IGrpcChannelFactory _channelFactory;

        private AccrualPeriod.AccrualPeriodClient _accrualPeriodClient = null;
        private string _token = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creat a new Grpc Provider instance
        /// </summary>
        /// <param name="channelFactory">gRPC Channel factory object instance</param>
        /// <param name="loggerFactory">Logger factory object</param>
        public GrpcAccrualPeriodServiceProvider
        (
            IGrpcChannelFactory channelFactory,
            ILoggerFactory loggerFactory
        )
        {
            _channelFactory = channelFactory;
            _logger = loggerFactory?.CreateLogger<GrpcAccrualPeriodServiceProvider>();
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public void SetToken(string token)
        {
            _token = token;
            _accrualPeriodClient = new AccrualPeriod.AccrualPeriodClient(_channelFactory.CreateChannel(_token));
        }

        ///<inheritdoc/>
        public async Task<StartPeriodResponse> StartPeriod(int year, int month)
        {

            PeriodRequest request = new PeriodRequest() { Year = year, Month = month };
            StartPeriodResponse resp;

            _logger?.LogInformation("Call StartPeriod on gRPC Service on {urlService}", _channelFactory.UrlServer);


            try
            {
                Empty reply = await _accrualPeriodClient.StartPeriodAsync(request);
                resp = new StartPeriodResponse(StatusCode.OK, null);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToStartPeriodResponseResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToStartPeriodResponseResponse();
            }

            _logger?.LogInformation("StartPeriod Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<ClosePeriodResponse> ClosePeriod(int year, int month)
        {

            PeriodRequest request = new PeriodRequest() { Year = year, Month = month };
            ClosePeriodResponse resp;

            _logger?.LogInformation("Call ClosePeriod on gRPC Service on {urlService}", _channelFactory.UrlServer);


            try
            {
                Empty reply = await _accrualPeriodClient.ClosePeriodAsync(request);
                resp = new ClosePeriodResponse(StatusCode.OK, null);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToClosePeriodResponseResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToClosePeriodResponseResponse();
            }

            _logger?.LogInformation("ClosePeriod Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<ListAccrualPeriodDetailResponse> ListPeriod()
        {
            Empty request = new Empty();
            ListAccrualPeriodDetailResponse resp;

            _logger?.LogInformation("Call ListPeriod on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                ListPeriodReply reply = await _accrualPeriodClient.ListPeriodAsync(request);
                resp = reply.ToListAccrualPeriodDetailResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToListAccrualPeriodDetailResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToListAccrualPeriodDetailResponse();
            }

            _logger?.LogInformation("ListPeriod Finished");

            return resp;
        }

        #endregion

    }
}
