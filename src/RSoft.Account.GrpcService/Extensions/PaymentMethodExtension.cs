using Google.Protobuf.WellKnownTypes;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Grpc.PaymentMethod;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Account.GrpcService.Extensions
{

    /// <summary>
    /// PaymentMethod extensions
    /// </summary>
    public static class PaymentMethodExtension
    {

        /// <summary>
        /// Map PaymentMethod dto to PaymentMethod-detail (grpc-model)
        /// </summary>
        /// <param name="dto">PaymentMethod dto instance</param>
        public static PaymentMethodDetail Map(this PaymentMethodDto dto)
        {
            PaymentMethodDetail result = null;
            if (dto != null)
            {
                result = new PaymentMethodDetail()
                {
                    Id = dto.Id.ToString(),
                    Name = dto.Name,
                    IsActive = dto.IsActive,
                    PaymentType = dto.PaymentType,
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
        /// Map PaymentMethod dto list to PaymentMethod-detail (grpc-model) list
        /// </summary>
        /// <param name="dtos">PaymentMethod dtos list</param>
        public static IEnumerable<PaymentMethodDetail> Map(this IEnumerable<PaymentMethodDto> dtos)
        {
            IEnumerable<PaymentMethodDetail> result = null;
            if (dtos?.Count() > 0)
                result = dtos.Select(d => d.Map());
            return result;
        }


    }
}
