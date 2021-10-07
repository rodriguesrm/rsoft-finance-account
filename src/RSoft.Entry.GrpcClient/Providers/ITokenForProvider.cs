namespace RSoft.Entry.GrpcClient.Providers
{

    /// <summary>
    /// Token access for providers contract interface
    /// </summary>
    public interface ITokenForProvider
    {

        /// <summary>
        /// Set token
        /// </summary>
        /// <param name="token">Token authorization</param>
        void SetToken(string token);

    }
}
