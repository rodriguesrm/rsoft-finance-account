using RSoft.Entry.Core.Entities;
using RSoft.Lib.Design.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// AccrualPeriod domain service interface
    /// </summary>
    public interface IAccrualPeriodDomainService : IDomainServiceBase<AccrualPeriod, Guid>
    {

        /// <summary>
        /// Get row by key values
        /// </summary>
        /// <param name="year">Year number</param>
        /// <param name="month">Month number</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<AccrualPeriod> GetByKeyAsync(int year, int month, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update entity on context
        /// </summary>
        /// <param name="year">Year number</param>
        /// <param name="month">Month number</param>
        /// <param name="entity">Entity to update</param>
        AccrualPeriod Update(int year, int month, AccrualPeriod entity);

        /// <summary>
        /// Delete entity from context
        /// </summary>
        /// <param name="year">Year number</param>
        /// <param name="month">Month number</param>
        void Delete(int year, int month);

    }

}