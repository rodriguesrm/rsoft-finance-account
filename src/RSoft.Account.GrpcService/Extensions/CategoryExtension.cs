using Google.Protobuf.WellKnownTypes;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Grpc.Category;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Account.GrpcService.Extensions
{

    /// <summary>
    /// Category extensions
    /// </summary>
    public static class CategoryExtension
    {

        /// <summary>
        /// Map category dto to category-detail (grpc-model)
        /// </summary>
        /// <param name="dto">Category dto instance</param>
        public static CategoryDetail Map(this CategoryDto dto)
        {
            CategoryDetail result = null;
            if (dto != null)
            {
                result = new CategoryDetail()
                {
                    Id = dto.Id.ToString(),
                    Name = dto.Name,
                    IsActive = dto.IsActive,
                    CreatedOn = Timestamp.FromDateTime(dto.CreatedBy.Date.ToUniversalTime()),
                    CreatedBy = new AuthorDetail()
                    {
                        Id = dto.CreatedBy.Id.ToString(),
                        Name = dto.CreatedBy.Name
                    }
                };
                if (dto.ChangedBy != null)
                {
                    result.ChangedOn = new NullableTimestamp()
                    {
                        Data = Timestamp.FromDateTime(dto.ChangedBy.Date.ToUniversalTime())
                    };
                    result.ChangedBy = new NullableAuthorDetail()
                    {
                        Data = new AuthorDetail()
                        {
                            Id = dto.ChangedBy.Id.ToString(),
                            Name = dto.ChangedBy.Name
                        }
                    };
                }
            }
            return result;
        }

        /// <summary>
        /// Map category dto list to category-detail (grpc-model) list
        /// </summary>
        /// <param name="dtos">Category dtos list</param>
        public static IEnumerable<CategoryDetail> Map(this IEnumerable<CategoryDto> dtos)
        {
            IEnumerable<CategoryDetail> result = null;
            if (dtos?.Count() > 0)
                result = dtos.Select(d => d.Map());
            return result;
        }


    }
}
