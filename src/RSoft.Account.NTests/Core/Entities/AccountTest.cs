using NUnit.Framework;
using RSoft.Account.NTests.DependencyInjection;
using System;
using System.Linq;
using AccountDomain = RSoft.Account.Core.Entities.Account;
using CategoryDomain = RSoft.Account.Core.Entities.Category;

namespace RSoft.Account.NTests.Core.Entities
{

    public class AccountTest : TestFor<AccountDomain>
    {

        #region Constructors

        public AccountTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Test]
        public void CreateAccountInstance_ResultSuccess()
        {

            AccountDomain account1 = new();
            AccountDomain account2 = new(Guid.NewGuid());
            AccountDomain account3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(account1);
            Assert.NotNull(account2);
            Assert.NotNull(account3);

        }

        [Test]
        public void ValidateAccountWhenDataIsInvalid_ResultInvalidTrue()
        {
            AccountDomain account = new();
            account.Validate();
            Assert.True(account.Invalid);
            Assert.AreEqual(2, account.Notifications.Count);
            Assert.True(account.Notifications.Any(n => n.Message == "FIELD_REQUIRED"));
            Assert.True(account.Notifications.Any(n => n.Message == "CATEGORY_REQUIRED"));
        }

        [Test]
        public void ValidateAccountWhenDataIsValid_ResultValidTrue()
        {
            AccountDomain account = One<AccountDomain>();
            account.Category = new CategoryDomain(Guid.NewGuid()) { Name = "**" };
            account.Name = "CategoryName";
            account.Validate();
            Assert.True(account.Valid);
        }

        #endregion

    }
}
