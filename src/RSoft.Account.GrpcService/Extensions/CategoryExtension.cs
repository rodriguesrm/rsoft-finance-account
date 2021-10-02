using Google.Protobuf.WellKnownTypes;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Grpc.Protobuf;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Entry.GrpcService.Extensions
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
            if (dto == null)
                return null;
            CategoryDetail reply = new();
            dto.Map(reply);
            return reply;
        }

        /// <summary>
        /// Map category dto to category-detail (grpc-model)
        /// </summary>
        /// <param name="dto">Category dto instance</param>
        /// <param name="reply">Reply object intance</param>
        public static void Map(this CategoryDto dto, CategoryDetail reply)
        {
            if (dto != null)
            {
                reply.Id = dto.Id.ToString();
                reply.Name = dto.Name;
                reply.IsActive = dto.IsActive;
                reply.CreatedOn = Timestamp.FromDateTime(dto.CreatedBy.Date.ToUniversalTime());
                reply.CreatedBy = new AuthorDetail()
                {
                    Id = dto.CreatedBy.Id.ToString(),
                    Name = dto.CreatedBy.Name
                };

                if (dto.ChangedBy != null)
                {
                    reply.ChangedOn = new NullableTimestamp()
                    {
                        Data = Timestamp.FromDateTime(dto.ChangedBy.Date.ToUniversalTime())
                    };
                    reply.ChangedBy = new NullableAuthorDetail()
                    {
                        Data = new AuthorDetail()
                        {
                            Id = dto.ChangedBy.Id.ToString(),
                            Name = dto.ChangedBy.Name
                        }
                    };
                }
            }
        }

        /// <summary>
        /// Map category dto list to category-detail (grpc-model) list
        /// </summary>
        /// <param name="dtos">Category dtos list</param>
        public static IEnumerable<CategoryDetail> Map(this IEnumerable<CategoryDto> dtos)
        {
            IEnumerable<CategoryDetail> result = new List<CategoryDetail>();
            if (dtos?.Count() > 0)
                result = dtos.Select(d => d.Map());
            return result;
        }


    }
}
