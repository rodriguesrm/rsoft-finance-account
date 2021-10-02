using Google.Protobuf.WellKnownTypes;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Grpc.Protobuf;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Entry.GrpcService.Extensions
{

    /// <summary>
    /// Account extensions
    /// </summary>
    public static class AccountExtension
    {

        /// <summary>
        /// Map Account dto to Account-detail (grpc-model)
        /// </summary>
        /// <param name="dto">Account dto instance</param>
        public static AccountDetail Map(this EntryDto dto)
        {
            if (dto == null) return null;
            AccountDetail reply = new();
            dto.Map(reply);
            return reply;
        }

        /// <summary>
        /// Map Account dto to Account-detail (grpc-model)
        /// </summary>
        /// <param name="dto">Account dto instance</param>
        /// <param name="reply">Account detail instance</param>
        public static void Map(this EntryDto dto, AccountDetail reply)
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
        /// Map Account dto list to Account-detail (grpc-model) list
        /// </summary>
        /// <param name="dtos">Account dtos list</param>
        public static IEnumerable<AccountDetail> Map(this IEnumerable<EntryDto> dtos)
        {
            IEnumerable<AccountDetail> result = new List<AccountDetail>();
            if (dtos?.Count() > 0)
                result = dtos.Select(d => d.Map());
            return result;
        }

    }
}
