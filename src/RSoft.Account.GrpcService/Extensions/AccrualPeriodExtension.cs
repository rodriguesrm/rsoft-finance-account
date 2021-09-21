using Google.Protobuf.WellKnownTypes;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Grpc.Protobuf;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Account.GrpcService.Extensions
{

    /// <summary>
    /// AccrualPeriod extensions
    /// </summary>
    public static class AccrualPeriodExtension
    {

        /// <summary>
        /// Map AccrualPeriod dto to AccrualPeriod-detail (grpc-model)
        /// </summary>
        /// <param name="dto">AccrualPeriod dto instance</param>
        public static AccrualPeriodDetail Map(this AccrualPeriodDto dto)
        {
            if (dto == null)
                return null;
            AccrualPeriodDetail reply = new();
            dto.Map(reply);
            return reply;
        }

        /// <summary>
        /// Map AccrualPeriod dto to AccrualPeriod-detail (grpc-model)
        /// </summary>
        /// <param name="dto">AccrualPeriod dto instance</param>
        /// <param name="reply">Reply object intance</param>
        public static void Map(this AccrualPeriodDto dto, AccrualPeriodDetail reply)
        {
            if (dto != null)
            {

                reply.Year = dto.Year;
                reply.Month = dto.Month;
                reply.OpeningBalance = dto.OpeningBalance;
                reply.TotalCredits = dto.TotalCredits;
                reply.TotalDebts = dto.TotalDebts;
                reply.AccrualPeriodBalance = dto.AccrualPeriodBalance;
                reply.ClosingBalance = dto.ClosingBalance;
                reply.IsClosed = dto.IsClosed;
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
                if (dto.ClosedAuthor != null)
                {
                    reply.ClosedAuthor = new NullableAuthorDetail()
                    {
                        Data = new AuthorDetail()
                        {
                            Id = dto.ClosedAuthor.Id.ToString(),
                            Name = dto.ClosedAuthor.Name
                        }
                    };
                }
            }
        }

        /// <summary>
        /// Map AccrualPeriod dto list to AccrualPeriod-detail (grpc-model) list
        /// </summary>
        /// <param name="dtos">AccrualPeriod dtos list</param>
        public static IEnumerable<AccrualPeriodDetail> Map(this IEnumerable<AccrualPeriodDto> dtos)
        {
            IEnumerable<AccrualPeriodDetail> result = new List<AccrualPeriodDetail>();
            if (dtos?.Count() > 0)
                result = dtos.Select(d => d.Map());
            return result;
        }


    }
}
