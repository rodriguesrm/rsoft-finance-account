using RSoft.Entry.GrpcClient.Models;
using System;
using System.Threading.Tasks;

namespace RSoft.Entry.GrpcClient.Providers
{

    /// <summary>
    /// RSoft gRpc Entry Service provider interface contract
    /// </summary>
    public interface IGrpcEntryServiceProvider : ITokenForProvider
    {

        /// <summary>
        /// Create Entry
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <param name="categoryId">Category id key value</param>
        Task<CreateEntryResponse> CreateEntry(string name, Guid? categoryId);

        /// <summary>
        /// Udpate an existing Entry
        /// </summary>
        /// <param name="id">Entry id key value</param>
        /// <param name="name">Entry name</param>
        /// <param name="categoryId">Category id key value</param>
        Task<UpdateEntryResponse> UpdateEntry(Guid id, string name, Guid? categoryId);

        /// <summary>
        /// Enable an existing Entry
        /// </summary>
        /// <param name="id">Entry id key value</param>
        Task<ChangeEntryStatusResponse> EnableEntry(Guid id);

        /// <summary>
        /// Disable an existing Entry
        /// </summary>
        /// <param name="id">Entry id key value</param>
        Task<ChangeEntryStatusResponse> DisableEntry(Guid id);

        /// <summary>
        /// Get Entry by id
        /// </summary>
        /// <param name="id">Entry id key value</param>
        Task<EntryDetailResponse> GetEntry(Guid id);

        /// <summary>
        /// Lista categories
        /// </summary>
        Task<ListEntryDetailResponse> ListEntry();

        
    }
}