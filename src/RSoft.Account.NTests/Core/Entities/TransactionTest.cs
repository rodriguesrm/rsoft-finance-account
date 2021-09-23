using TransactionDomain = RSoft.Account.Core.Entities.Transaction;
using AccountDomain = RSoft.Account.Core.Entities.Account;
using System;
using System.Linq;
using NUnit.Framework;
using RSoft.Finance.Contracts.Enum;
using RSoft.Account.Core.Entities;
using RSoft.Account.NTests.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.NTests.Core.Entities
{

    [ExcludeFromCodeCoverage(Justification = "Test class should not be considered in test coverage.")]
    public class TransactionTest : TestFor<TransactionDomain>
    {

        #region Constructors

        public TransactionTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Test]
        public void CreateTransactionInstance_ResultSuccess()
        {

            TransactionDomain transaction1 = new();
            TransactionDomain transaction2 = new(Guid.NewGuid());
            TransactionDomain transaction3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(transaction1);
            Assert.NotNull(transaction2);
            Assert.NotNull(transaction3);

        }

        [Test]
        public void ValidateTransactionWhenDataIsInvalid_ResultInvalidTrue()
        {
            TransactionDomain transaction = new();
            transaction.Validate();
            Assert.True(transaction.Invalid);
            Assert.AreEqual(5, transaction.Notifications.Count);
            Assert.True(transaction.Notifications.Any(n => n.Message == "GREATER_THAN_ZERO"));
            Assert.True(transaction.Notifications.Any(n => n.Message == "DATE_REQUIRED"));
            Assert.True(transaction.Notifications.Any(n => n.Message == "FIELD_REQUIRED"));
            Assert.True(transaction.Notifications.Any(n => n.Message == "ACCOUNT_REQUIRED"));
            Assert.True(transaction.Notifications.Any(n => n.Message == "PAYMENTMETHOD_REQUIRED"));
        }

        [Test]
        public void ValidateTransactionWhenDataIsValid_ResultValidTrue()
        {
            float amount = 450f;
            string comment = "COMMENT TEST";
            DateTime date = DateTime.UtcNow.AddMinutes(-1);
            TransactionDomain transaction = new()
            {
                Date = date,
                TransactionType = TransactionTypeEnum.Debt,
                Amount = amount,
                Comment = comment,
                PaymentMethod = new PaymentMethod(Guid.NewGuid()) { Name = "PAYMENT_METHOD_NAME" },
                Account = new AccountDomain(Guid.NewGuid()) { Name = "ACCOUNT_NAME" }
            };
            transaction.Validate();
            Assert.True(transaction.Valid);
            Assert.AreEqual(transaction.Year, date.Year);
            Assert.AreEqual(transaction.Month, date.Month);
        }

        #endregion

    }
}
