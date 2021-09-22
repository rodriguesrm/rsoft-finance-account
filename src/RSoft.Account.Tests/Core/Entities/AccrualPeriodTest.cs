using System;
using Xunit;
using RSoft.Account.Test.DependencyInjection;
using RSoft.Account.Core.Entities;

namespace RSoft.Account.Test.Core.Entities
{

    /// <summary>
    /// AccrualPeriod entity tests
    /// </summary>
    public class AccrualPeriodTest
    {

        #region Constructors

        public AccrualPeriodTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Fact]
        public void CreateAccrualPeriodInstance_ResultSuccess()
        {
            AccrualPeriod AccrualPeriod1 = new();
            AccrualPeriod AccrualPeriod2 = new(DateTime.UtcNow.Year, DateTime.UtcNow.Month);
            Assert.NotNull(AccrualPeriod1);
            Assert.NotNull(AccrualPeriod2);
        }

        [Fact]
        public void ValidateAccrualPeriodWhenDataIsInvalid_ResultInvalidTrue()
        {
            AccrualPeriod AccrualPeriod = new();
            AccrualPeriod.Validate();
            Assert.True(AccrualPeriod.Invalid);
            Assert.Equal(2, AccrualPeriod.Notifications.Count);
            Assert.Contains(AccrualPeriod.Notifications, n => n.Message == "INVALID_YEAR");
            Assert.Contains(AccrualPeriod.Notifications, n => n.Message == "INVALID_MONTH");
        }

        [Fact]
        public void ValidateAccrualPeriodWhenDataIsValid_ResultValidTrue()
        {
            AccrualPeriod AccrualPeriod = new(DateTime.UtcNow.Year, DateTime.UtcNow.Month);
            AccrualPeriod.Validate();
            Assert.True(AccrualPeriod.Valid);
        }

        [Fact]
        public void AccrualPeriodoBalance_CheckedOk()
        {
            Guid userId = Guid.NewGuid();
            const float openingBalance = 1200f;
            const float totalCredits = 1500f;
            const float totalDebts = 750f;
            const float accrualPeriodBalance = totalCredits - totalDebts;
            const float closingBalance = openingBalance + accrualPeriodBalance;
            AccrualPeriod accrualPeriod = new(DateTime.UtcNow.Year, DateTime.UtcNow.Month)
            {
                OpeningBalance = openingBalance
            };
            accrualPeriod.CloseAccrualPeriod(userId, totalCredits, totalDebts);
            Assert.True(accrualPeriod.IsClosed);
            Assert.True(accrualPeriod.Valid);
            Assert.Equal(openingBalance, accrualPeriod.OpeningBalance);
            Assert.Equal(totalCredits, accrualPeriod.TotalCredits);
            Assert.Equal(totalDebts, accrualPeriod.TotalDebts);
            Assert.Equal(accrualPeriodBalance, accrualPeriod.AccrualPeriodBalance);
            Assert.Equal(closingBalance, accrualPeriod.ClosingBalance);
        }

        #endregion

    }
}
