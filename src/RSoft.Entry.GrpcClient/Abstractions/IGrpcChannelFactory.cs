using Grpc.Net.Client;

namespace RSoft.Entry.GrpcClient.Abstractions
{

    /// <summary>
    /// Channel factory contract interface
    /// </summary>
    public interface IGrpcChannelFactory
    {

        /// <summary>
        /// Url server/host address
        /// </summary>
        string UrlServer { get; }


        /// <summary>
        /// Create gRPC Channel for client
        /// </summary>
        /// <param name="token">Token authentication</param>
        GrpcChannel CreateChannel(string token = null);

    }
}
