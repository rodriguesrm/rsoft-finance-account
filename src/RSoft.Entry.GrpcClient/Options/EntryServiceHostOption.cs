using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.GrpcClient.Options
{


    /// <summary>
    /// Entry service host options configuration model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EntryServiceHostOption
    {

        /// <summary>
        /// Host/Server address
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Host/Server port
        /// </summary>
        public int Port { get; set; }


        /// <summary>
        /// Get full server address
        /// </summary>
        public string GetFullAddress()
            => $"{Server}:{Port}";

    }
}
