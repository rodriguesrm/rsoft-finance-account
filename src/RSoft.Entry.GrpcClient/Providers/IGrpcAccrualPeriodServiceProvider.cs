using RSoft.Entry.GrpcClient.Models;
using System.Threading.Tasks;

namespace RSoft.Entry.GrpcClient.Providers
{

    /// <summary>
    /// RSoft gRpc AccrualPeriod Service provider interface contract
    /// </summary>
    public interface IGrpcAccrualPeriodServiceProvider : ITokenForProvider
    {

        /// <summary>
        /// Start accrual period
        /// </summary>
        /// <param name="year">Year number</param>
        /// <param name="month">Month number</param>
        Task<StartPeriodResponse> StartPeriod(int year, int month);

        /// <summary>
        /// Close accrual period
        /// </summary>
        /// <param name="year">Year number</param>
        /// <param name="month">Month number</param>
        Task<ClosePeriodResponse> ClosePeriod(int year, int month);

        /// <summary>
        /// Lista categories
        /// </summary>
        Task<ListAccrualPeriodDetailResponse> ListPeriod();

    }
}
