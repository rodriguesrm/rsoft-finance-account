using System;
using Xunit;
using RSoft.Account.Core.Entities;
using RSoft.Finance.Contracts.Enum;
using AccountDomain = RSoft.Account.Core.Entities.Account;
using RSoft.Account.Tests;

namespace RSoft.Account.Test.Core.Entities
{

    /// <summary>
    /// Transaction entity tests
    /// </summary>
    public class TransactionTest : TestBase
    {

        #region Constructors

        public TransactionTest() : base() { }

        #endregion

        #region Tests

        [Fact]
        public void CreateTransactionInstance_ResultSuccess()
        {

            Transaction Transaction1 = new();
            Transaction Transaction2 = new(Guid.NewGuid());
            Transaction Transaction3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(Transaction1);
            Assert.NotNull(Transaction2);
            Assert.NotNull(Transaction3);

        }

        [Fact]
        public void ValidateTransactionWhenDataIsInvalid_ResultInvalidTrue()
        {
            Transaction Transaction = new();
            Transaction.Validate();
            Assert.True(Transaction.Invalid);
            Assert.Equal(5, Transaction.Notifications.Count);
            Assert.Contains(Transaction.Notifications, n => n.Message == "GREATER_THAN_ZERO");
            Assert.Contains(Transaction.Notifications, n => n.Message == "DATE_REQUIRED");
            Assert.Contains(Transaction.Notifications, n => n.Message == "FIELD_REQUIRED"); 
            Assert.Contains(Transaction.Notifications, n => n.Message == "ACCOUNT_REQUIRED");
            Assert.Contains(Transaction.Notifications, n => n.Message == "PAYMENTMETHOD_REQUIRED");
        }

        [Fact]
        public void ValidateTransactionWhenDataIsValid_ResultValidTrue()
        {
            float amount = 450f;
            string comment = "COMMENT TEST";
            DateTime date = DateTime.UtcNow.AddMinutes(-1);
            Transaction transaction = new()
            {
                Date = date,
                TransactionType= TransactionTypeEnum.Debt,
                Amount = amount,
                Comment = comment,
                PaymentMethod = new PaymentMethod(Guid.NewGuid()) { Name = "PAYMENT_METHOD_NAME" },
                Account = new AccountDomain(Guid.NewGuid()) { Name = "ACCOUNT_NAME" }
            };
            transaction.Validate();
            Assert.True(transaction.Valid);
            Assert.Equal(transaction.Year, date.Year);
            Assert.Equal(transaction.Month, date.Month);
        }

        #endregion

    }
}
