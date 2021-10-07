using Google.Protobuf.WellKnownTypes;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Grpc.Protobuf;
using RSoft.Helpers.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Entry.GrpcService.Extensions
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
        /// <param name="detail">PaymentMethod detail</param>
        public static void Map(this PaymentMethodDto dto, PaymentMethodDetail detail)
        {

            if (dto != null)
            {

                detail.Id = dto.Id.ToString();
                detail.Name = dto.Name;
                detail.IsActive = dto.IsActive;
                detail.PaymentType = new SimpleIdName()
                {
                    Id = ((int)dto.PaymentType).ToString(),
                    Name = dto.PaymentType.GetDescription()
                };
                detail.CreatedOn = Timestamp.FromDateTime(dto.CreatedBy.Date.ToUniversalTime());
                detail.CreatedBy = new AuthorDetail()
                {
                    Id = dto.CreatedBy.Id.ToString(),
                    Name = dto.CreatedBy.Name
                };

                if (dto.ChangedBy != null)
                {
                    detail.ChangedOn = new NullableTimestamp()
                    {
                        Data = Timestamp.FromDateTime(dto.ChangedBy.Date.ToUniversalTime())
                    };
                    detail.ChangedBy = new NullableAuthorDetail()
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
        /// Map PaymentMethod dto to PaymentMethod-detail (grpc-model)
        /// </summary>
        /// <param name="dto">PaymentMethod dto instance</param>
        public static PaymentMethodDetail Map(this PaymentMethodDto dto)
        {
            PaymentMethodDetail result = new PaymentMethodDetail();
            Map(dto, result);
            return result;
        }

        /// <summary>
        /// Map PaymentMethod dto list to PaymentMethod-detail (grpc-model) list
        /// </summary>
        /// <param name="dtos">PaymentMethod dtos list</param>
        public static IEnumerable<PaymentMethodDetail> Map(this IEnumerable<PaymentMethodDto> dtos)
        {
            IEnumerable<PaymentMethodDetail> result = new List<PaymentMethodDetail>();
            if (dtos?.Count() > 0)
                result = dtos.Select(d => d.Map());
            return result;
        }


    }
}
