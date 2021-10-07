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
    /// RSoft gRpc Category Service provider
    /// </summary>
    internal class GrpcCategoryServiceProvider : IGrpcCategoryServiceProvider
    {

        #region Local objects/variables

        private readonly ILogger<GrpcCategoryServiceProvider> _logger;
        private readonly IGrpcChannelFactory _channelFactory;

        private Category.CategoryClient _categoryClient = null;
        private string _token = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creat a new Grpc Provider instance
        /// </summary>
        /// <param name="channelFactory">gRPC Channel factory object instance</param>
        /// <param name="loggerFactory">Logger factory object</param>
        public GrpcCategoryServiceProvider
        (
            IGrpcChannelFactory channelFactory,
            ILoggerFactory loggerFactory
        )
        {
            _channelFactory = channelFactory;
            _logger = loggerFactory?.CreateLogger<GrpcCategoryServiceProvider>();
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public void SetToken(string token)
        {
            _token = token;
            _categoryClient = new Category.CategoryClient(_channelFactory.CreateChannel(_token));
        }

        ///<inheritdoc/>
        public async Task<CreateCategoryResponse> CreateCategory(string name)
        {

            CreateCategoryResponse resp;
            CreateCategoryRequest request =
                new CreateCategoryRequest() { Name = name };

            _logger?.LogInformation("Call CreateCategory on gRPC Service on {urlService}", _channelFactory.UrlServer);
            try
            {
                CreateCategoryReply reply = await _categoryClient.CreateCategoryAsync(request);
                resp = reply.ToCreateCategoryResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToCreateCategoryResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToCreateCategoryResponse();
            }

            _logger?.LogInformation("CreateCategory Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<UpdateCategoryResponse> UpdateCategory(Guid id, string name)
        {

            UpdateCategoryResponse resp;
            UpdateCategoryRequest request =
                new UpdateCategoryRequest() { Id = id.ToString(), Name = name };

            _logger?.LogInformation("Call UpdateCategory on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                _ = await _categoryClient.UpdateCategoryAsync(request);
                resp = new UpdateCategoryResponse(StatusCode.OK);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToUpdateCategoryResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToUpdateCategoryResponse();
            }

            _logger?.LogInformation("UpdateCategory Finished");

            return resp;
        }

        ///<inheritdoc/>
        public async Task<ChangeCategoryStatusResponse> EnableCategory(Guid id)
        {

            ChangeCategoryStatusResponse resp;
            EnableCategoryRequest request = new EnableCategoryRequest() { Id = id.ToString() };

            _logger?.LogInformation("Call EnableCategory on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                _ = await _categoryClient.EnableCategoryAsync(request);
                resp = new ChangeCategoryStatusResponse(StatusCode.OK);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToChangeCategoryStatusResponse(nameof(EnableCategory));
            }
            catch (Exception ex)
            {
                resp = ex.ToChangeCategoryStatusResponse();
            }

            _logger?.LogInformation("EnableCategory Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<ChangeCategoryStatusResponse> DisableCategory(Guid id)
        {

            ChangeCategoryStatusResponse resp;
            DisableCategoryRequest request = new DisableCategoryRequest() { Id = id.ToString() };

            _logger?.LogInformation("Call EnableCategory on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                _ = await _categoryClient.DisableCategoryAsync(request);
                resp = new ChangeCategoryStatusResponse(StatusCode.OK);
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToChangeCategoryStatusResponse(nameof(DisableCategory));
            }
            catch (Exception ex)
            {
                resp = ex.ToChangeCategoryStatusResponse();
            }

            _logger?.LogInformation("DisableCategory Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<CategoryDetailResponse> GetCategory(Guid id)
        {
            GetCategoryRequest request = new GetCategoryRequest() { Id = id.ToString() };
            CategoryDetailResponse resp;

            _logger?.LogInformation("Call GetCategory on gRPC Service on {urlService}", _channelFactory.UrlServer);


            try
            {
                CategoryDetail reply = await _categoryClient.GetCategoryAsync(request);
                resp = reply.ToCategoryDetailResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToCategoryDetailResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToCategoryDetailResponse();
            }

            _logger?.LogInformation("GetCategory Finished");

            return resp;
        }

        ///<inheritdoc/>
        public async Task<ListCategoryDetailResponse> ListCategory()
        {

            Empty request = new Empty();
            ListCategoryDetailResponse resp;

            _logger?.LogInformation("Call ListCategory on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                ListCategoryReply reply = await _categoryClient.ListCategoryAsync(request);
                resp = reply.ToListCategoryDetailResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToListCategoryDetailResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToListCategoryDetailResponse();
            }

            _logger?.LogInformation("ListCategory Finished");

            return resp;
        }

        #endregion

    }
}