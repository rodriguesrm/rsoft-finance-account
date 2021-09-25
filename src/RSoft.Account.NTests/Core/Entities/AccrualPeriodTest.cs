using NUnit.Framework;
using RSoft.Account.Core.Entities;
using RSoft.Account.NTests.DependencyInjection;
using RSoft.Lib.Common.ValueObjects;
using System;
using System.Linq;

namespace RSoft.Account.NTests.Core.Entities
{

    public class AccrualPeriodTest : TestFor<AccrualPeriod>
    {

        #region Constructors

        public AccrualPeriodTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Test]
        public void CreateAccrualPeriodInstance_ResultSuccess()
        {
            AccrualPeriod accrualPeriod1 = new();
            AccrualPeriod AccrualPeriod2 = new(DateTime.UtcNow.Year, DateTime.UtcNow.Month);
            Assert.NotNull(accrualPeriod1);
            Assert.NotNull(AccrualPeriod2);
        }

        [Test]
        public void ValidateAccrualPeriodWhenDataIsInvalid_ResultInvalidTrue()
        {
            AccrualPeriod accrualPeriod = new();
            accrualPeriod.Validate();
            Assert.True(accrualPeriod.Invalid);
            Assert.AreEqual(2, accrualPeriod.Notifications.Count);
            Assert.True(accrualPeriod.Notifications.Any(n => n.Message == "INVALID_YEAR"));
            Assert.True(accrualPeriod.Notifications.Any(n => n.Message == "INVALID_MONTH"));
        }

        [Test]
        public void ValidateAccrualPeriodWhenDataIsValid_ResultValidTrue()
        {
            AccrualPeriod accrualPeriod = new(DateTime.UtcNow.Year, DateTime.UtcNow.Month)
            {
                CreatedAuthor = One<Author<Guid>>()
            };
            accrualPeriod.Validate();
            Assert.True(accrualPeriod.Valid);
        }

        [Test]
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
            Assert.False(accrualPeriod.IsClosed);
            Assert.AreEqual(0, accrualPeriod.ClosingBalance);
            accrualPeriod.CloseAccrualPeriod(userId, totalCredits, totalDebts);
            Assert.True(accrualPeriod.IsClosed);
            Assert.True(accrualPeriod.Valid);
            Assert.AreEqual(openingBalance, accrualPeriod.OpeningBalance);
            Assert.AreEqual(totalCredits, accrualPeriod.TotalCredits);
            Assert.AreEqual(totalDebts, accrualPeriod.TotalDebts);
            Assert.AreEqual(accrualPeriodBalance, accrualPeriod.AccrualPeriodBalance);
            Assert.AreEqual(closingBalance, accrualPeriod.ClosingBalance);
        }

        #endregion

    }
}
