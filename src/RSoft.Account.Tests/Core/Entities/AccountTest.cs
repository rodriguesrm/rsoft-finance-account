using System;
using AccountDomain = RSoft.Account.Core.Entities.Account;
using CategoryDomain = RSoft.Account.Core.Entities.Category;
using Xunit;
using RSoft.Account.Test.DependencyInjection;

namespace RSoft.Account.Test.Core.Entities
{

    /// <summary>
    /// Account entity tests
    /// </summary>
    public class AccountTest
    {

        #region Constructors

        public AccountTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Fact]
        public void CreateAccountInstance_ResultSuccess()
        {

            AccountDomain account1 = new();
            AccountDomain account2 = new(Guid.NewGuid());
            AccountDomain account3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(account1);
            Assert.NotNull(account2);
            Assert.NotNull(account3);

        }

        [Fact]
        public void ValidateAccountWhenDataIsInvalid_ResultInvalidTrue()
        {
            AccountDomain account = new();
            account.Validate();
            Assert.True(account.Invalid);
            Assert.Equal(2, account.Notifications.Count);
            Assert.Contains(account.Notifications, n => n.Message == "FIELD_REQUIRED");
            Assert.Contains(account.Notifications, n => n.Message == "CATEGORY_REQUIRED");
        }

        [Fact]
        public void ValidateAccountWhenDataIsValid_ResultValidTrue()
        {
            AccountDomain account = new()
            {
                Category = new CategoryDomain(Guid.NewGuid()) { Name = "**" },
                Name = "CategoryName"
            };
            account.Validate();
            Assert.True(account.Valid);
        }

        #endregion

    }
}
