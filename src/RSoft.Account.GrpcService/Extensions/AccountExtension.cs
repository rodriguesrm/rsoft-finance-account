using Google.Protobuf.WellKnownTypes;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Grpc.Protobuf;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Account.GrpcService.Extensions
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
        public static AccountDetail Map(this AccountDto dto)
        {
            AccountDetail result = null;
            if (dto != null)
            {

                result = new AccountDetail()
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

                if (dto.Category != null)
                {
                    result.Category = new NullableCategoryInfo()
                    {
                        Data = new CategoryInfo()
                        {
                            Id = dto.Category.Id.ToString(),
                            Name = dto.Category.Name
                        }
                    };
                }

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
        /// Map Account dto list to Account-detail (grpc-model) list
        /// </summary>
        /// <param name="dtos">Account dtos list</param>
        public static IEnumerable<AccountDetail> Map(this IEnumerable<AccountDto> dtos)
        {
            IEnumerable<AccountDetail> result = null;
            if (dtos?.Count() > 0)
                result = dtos.Select(d => d.Map());
            return result;
        }

    }
}
