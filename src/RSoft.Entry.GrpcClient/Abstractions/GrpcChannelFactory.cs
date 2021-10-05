using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RSoft.Entry.GrpcClient.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RSoft.Entry.GrpcClient.Abstractions
{

    /// <summary>
    /// Channel factory contract class
    /// </summary>
    public class GrpcChannelFactory : IGrpcChannelFactory
    {

        #region Local objects/variables

        private readonly string _urlServer;
        private readonly bool _isProduction;

        #endregion

        #region Constructors

        /// <summary>
        /// Create gRPC channel factory instance
        /// </summary>
        /// <param name="serviceHostOptions">Entry service host options/parameters</param>
        public GrpcChannelFactory(IOptions<EntryServiceHostOption> serviceHostOptions)
        {
            _urlServer = serviceHostOptions.Value.GetFullAddress();
            _isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Production;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Url server/host address
        /// </summary>
        public string UrlServer => _urlServer;

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public GrpcChannel CreateChannel(string token = null)
        {

            CallCredentials credentials = CallCredentials.FromInterceptor((context, metadata) =>
            {
                if (!string.IsNullOrEmpty(token))
                {
                    metadata.Add("Authorization", $"Bearer {token}");
                }
                return Task.CompletedTask;
            });


            GrpcChannel channel;
            if (_isProduction)
            {
                channel = GrpcChannel.ForAddress(_urlServer, new GrpcChannelOptions
                {
                    Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
                });
            }
            else
            {
                channel = GrpcChannel.ForAddress(_urlServer, new GrpcChannelOptions
                {
                    HttpClient = new HttpClient(new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                    }),
                    Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
                });
            }

            return channel;

        }

        #endregion

    }
}
