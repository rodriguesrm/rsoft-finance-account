using RSoft.Entry.GrpcClient.Models;
using System;
using System.Threading.Tasks;

namespace RSoft.Entry.GrpcClient.Providers
{

    /// <summary>
    /// RSoft gRpc Category Service provider interface contract
    /// </summary>
    public interface IGrpcCategoryServiceProvider : ITokenForProvider
    {

        /// <summary>
        /// Create category
        /// </summary>
        /// <param name="name">Category name</param>
        Task<CreateCategoryResponse> CreateCategory(string name);

        /// <summary>
        /// Udpate an existing category
        /// </summary>
        /// <param name="id">Category id key value</param>
        /// <param name="name">Category name</param>
        Task<UpdateCategoryResponse> UpdateCategory(Guid id, string name);

        /// <summary>
        /// Enable an existing category
        /// </summary>
        /// <param name="id">Category id key value</param>
        Task<ChangeCategoryStatusResponse> EnableCategory(Guid id);

        /// <summary>
        /// Disable an existing category
        /// </summary>
        /// <param name="id">Category id key value</param>
        Task<ChangeCategoryStatusResponse> DisableCategory(Guid id);

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id">Category id key value</param>
        Task<CategoryDetailResponse> GetCategory(Guid id);

        /// <summary>
        /// Lista categories
        /// </summary>
        Task<ListCategoryDetailResponse> ListCategory();

        
    }
}