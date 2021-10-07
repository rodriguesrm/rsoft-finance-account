using Grpc.Core;
using Microsoft.Extensions.Logging;
using RSoft.Entry.GrpcClient.Abstractions;
using RSoft.Entry.GrpcClient.Extensions;
using RSoft.Entry.GrpcClient.Models;
using System;
using System.Threading.Tasks;
using RSoft.Entry.Grpc.Protobuf;
using Google.Protobuf.WellKnownTypes;
using proto = RSoft.Entry.Grpc.Protobuf;

namespace RSoft.Entry.GrpcClient.Providers
{

    /// <summary>
    /// RSoft gRpc Entry Service provider
    /// </summary>
    internal class GrpcEntryServiceProvider : IGrpcEntryServiceProvider
    {

        #region Local objects/variables

        private readonly ILogger<GrpcEntryServiceProvider> _logger;
        private readonly IGrpcChannelFactory _channelFactory;

        private proto.Entry.EntryClient _entryClient = null;
        private string _token = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creat a new Grpc Provider instance
        /// </summary>
        /// <param name="channelFactory">gRPC Channel factory object instance</param>
        /// <param name="loggerFactory">Logger factory object</param>
        public GrpcEntryServiceProvider
        (
            IGrpcChannelFactory channelFactory,
            ILoggerFactory loggerFactory
        )
        {
            _channelFactory = channelFactory;
            _logger = loggerFactory?.CreateLogger<GrpcEntryServiceProvider>();
        }

        #endregion

        #region Local methods

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public void SetToken(string token)
        {
            _token = token;
            _entryClient = new proto.Entry.EntryClient(_channelFactory.CreateChannel(_token));
        }

        ///<inheritdoc/>
        public async Task<CreateEntryResponse> CreateEntry(string name, Guid? categoryId)
        {

            CreateEntryResponse resp;
            CreateEntryRequest request =
                new CreateEntryRequest() 
                { 
                    Name = name,
                    CategoryId = categoryId?.ToString()
                };

            _logger?.LogInformation("Call CreateEntry on gRPC Service on {urlService}", _channelFactory.UrlServer);
            try
            {
                CreateEntryReply reply = await _entryClient.CreateEntryAsync(request);
                resp = reply.ToCreateEntryResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToCreateEntryResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToCreateEntryResponse();
            }

            _logger?.LogInformation("CreateEntry Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<UpdateEntryResponse> UpdateEntry(Guid id, string name, Guid? categoryId)
        {

            UpdateEntryResponse resp;
            UpdateEntryRequest request =
                new UpdateEntryRequest() 
                { 
                    Id = id.ToString(), 
                    Name = name ,
                    CategoryId = categoryId?.ToString()
                };

            _logger?.LogInformation("Call UpdateEntry on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                _ = await _entryClient.UpdateEntryAsync(request);
                resp = new UpdateEntryResponse(StatusCode.OK);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToUpdateEntryResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToUpdateEntryResponse();
            }

            _logger?.LogInformation("UpdateEntry Finished");

            return resp;
        }

        ///<inheritdoc/>
        public async Task<ChangeEntryStatusResponse> EnableEntry(Guid id)
        {

            ChangeEntryStatusResponse resp;
            ChangeStatusEntryRequest request = new ChangeStatusEntryRequest() { Id = id.ToString() };

            _logger?.LogInformation("Call EnableEntry on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                _ = await _entryClient.EnableEntryAsync(request);
                resp = new ChangeEntryStatusResponse(StatusCode.OK);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToChangeEntryStatusResponse(nameof(EnableEntry));
            }
            catch (Exception ex)
            {
                resp = ex.ToChangeEntryStatusResponse();
            }

            _logger?.LogInformation("EnableEntry Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<ChangeEntryStatusResponse> DisableEntry(Guid id)
        {

            ChangeEntryStatusResponse resp;
            ChangeStatusEntryRequest request = new ChangeStatusEntryRequest() { Id = id.ToString() };

            _logger?.LogInformation("Call EnableEntry on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                _ = await _entryClient.DisableEntryAsync(request);
                resp = new ChangeEntryStatusResponse(StatusCode.OK);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToChangeEntryStatusResponse(nameof(DisableEntry));
            }
            catch (Exception ex)
            {
                resp = ex.ToChangeEntryStatusResponse();
            }

            _logger?.LogInformation("DisableEntry Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<EntryDetailResponse> GetEntry(Guid id)
        {
            GetEntryRequest request = new GetEntryRequest() { Id = id.ToString() };
            EntryDetailResponse resp;

            _logger?.LogInformation("Call GetEntry on gRPC Service on {urlService}", _channelFactory.UrlServer);


            try
            {
                EntryDetail reply = await _entryClient.GetEntryAsync(request);
                resp = reply.ToEntryDetailResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToEntryDetailResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToEntryDetailResponse();
            }

            _logger?.LogInformation("GetEntry Finished");

            return resp;
        }

        ///<inheritdoc/>
        public async Task<ListEntryDetailResponse> ListEntry()
        {

            Empty request = new Empty();
            ListEntryDetailResponse resp;

            _logger?.LogInformation("Call ListEntry on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                ListEntryReply reply = await _entryClient.ListEntryAsync(request);
                resp = reply.ToListEntryDetailResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToListEntryDetailResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToListEntryDetailResponse();
            }

            _logger?.LogInformation("ListEntry Finished");

            return resp;
        }

        #endregion

    }
}