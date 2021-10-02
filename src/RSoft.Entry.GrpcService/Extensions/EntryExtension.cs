using Google.Protobuf.WellKnownTypes;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Grpc.Protobuf;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Entry.GrpcService.Extensions
{

    /// <summary>
    /// Entry extensions
    /// </summary>
    public static class EntryExtension
    {

        /// <summary>
        /// Map Entry dto to Entry-detail (grpc-model)
        /// </summary>
        /// <param name="dto">Entry dto instance</param>
        public static EntryDetail Map(this EntryDto dto)
        {
            if (dto == null) return null;
            EntryDetail reply = new();
            dto.Map(reply);
            return reply;
        }

        /// <summary>
        /// Map Entry dto to Entry-detail (grpc-model)
        /// </summary>
        /// <param name="dto">Entry dto instance</param>
        /// <param name="reply">Entry detail instance</param>
        public static void Map(this EntryDto dto, EntryDetail reply)
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

                if (dto.Category != null)
                {
                    reply.Category = new NullableSimpleIdName()
                    {
                        Data = new SimpleIdName()
                        {
                            Id = dto.Category.Id.ToString(),
                            Name = dto.Category.Name
                        }
                    };
                }

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
        /// Map Entry dto list to Entry-detail (grpc-model) list
        /// </summary>
        /// <param name="dtos">Entry dtos list</param>
        public static IEnumerable<EntryDetail> Map(this IEnumerable<EntryDto> dtos)
        {
            IEnumerable<EntryDetail> result = new List<EntryDetail>();
            if (dtos?.Count() > 0)
                result = dtos.Select(d => d.Map());
            return result;
        }

    }
}
