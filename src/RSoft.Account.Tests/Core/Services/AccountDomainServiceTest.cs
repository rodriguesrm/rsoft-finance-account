using Moq;
using RSoft.Account.Core.Ports;
using RSoft.Account.Core.Services;
using RSoft.Account.Tests.Stubs;
using RSoft.Lib.Common.Contracts.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using AccountDomain = RSoft.Account.Core.Entities.Account;

namespace RSoft.Account.Tests.Core.Services
{

    public class AccountDomainServiceTest : TestBase
    {

        #region Local Objects/Variables

        IAuthenticatedUser _authenticatedUser = new AuthenticatedUserStub();

        #endregion

        #region Constructors

        public AccountDomainServiceTest() : base()
        {

            
        }

        #endregion

        #region Constructors

        [Fact]
        public void AddAsync_ReturnEntitySaved()
        {
            Mock<IAccountProvider> provider = new();
            AccountDomain account = One<AccountDomain>();
            provider
                .Setup(d => d.AddAsync(It.IsAny<AccountDomain>(), It.IsAny<CancellationToken>()).Result)
                .Returns(account);
            AccountDomainService domainService = new(provider.Object, _authenticatedUser);
            AccountDomain result = domainService.AddAsync(account, default).Result;
            Assert.NotNull(result);
            Assert.Equal(account.Id, result.Id);
            Assert.Equal(account.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void Update_ReturnEntityUpdated()
        {
            Mock<IAccountProvider> provider = new();
            AccountDomain account = One<AccountDomain>();
            provider
                .Setup(d => d.Update(It.IsAny<Guid>(), It.IsAny<AccountDomain>()))
                .Returns(account);
            AccountDomainService domainService = new(provider.Object, _authenticatedUser);
            AccountDomain result = domainService.Update(account.Id, account);
            Assert.NotNull(result);
            Assert.Equal(account.Id, result.Id);
            Assert.Equal(account.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void GetAccountById_ReturnAccount()
        {
            Mock<IAccountProvider> provider = new();
            AccountDomain account = One<AccountDomain>();
            provider
                .Setup(d => d.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()).Result)
                .Returns(account);
            AccountDomainService domainService = new(provider.Object, _authenticatedUser);
            AccountDomain result = domainService.GetByKeyAsync(account.Id, default).Result;
            Assert.NotNull(result);
            Assert.Equal(account.Id, result.Id);
            Assert.Equal(account.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void ListAccount_ReturnAllAccounts()
        {
            Mock<IAccountProvider> provider = new();
            IEnumerable<AccountDomain> accounts = new List<AccountDomain>()
            {
                One<AccountDomain>(), One<AccountDomain>(), One<AccountDomain>()
            };
            IEnumerable<Guid> ids = accounts.Select(s => s.Id).ToList();
            provider
                .Setup(d => d.GetAllAsync(It.IsAny<CancellationToken>()).Result)
                .Returns(accounts);
            AccountDomainService domainService = new(provider.Object, _authenticatedUser);
            IEnumerable<AccountDomain> result = domainService.GetAllAsync(default).Result;
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            foreach (Guid id in ids)
            {
                Assert.Contains(result, c => c.Id == id);
            }
        }

        [Fact]
        public void DeleteAccount_SucessOnDeleteAndReturnNullWhenGetRemovedEntity()
        {
            Mock<IAccountProvider> provider = new();
            IList<AccountDomain> accounts = new List<AccountDomain>()
            {
                One<AccountDomain>(), One<AccountDomain>(), One<AccountDomain>()
            };
            AccountDomain account = accounts.First();
            provider
                .Setup(d => d.Delete(It.IsAny<Guid>()))
                .Callback<Guid>(id => 
                {
                    AccountDomain account = accounts.Where(x => x.Id == id).FirstOrDefault();
                    if (account != null)
                        accounts.Remove(account);
                });
            AccountDomainService domainService = new(provider.Object, _authenticatedUser);
            domainService.Delete(account.Id);
            Assert.Equal(2, accounts.Count());
            Assert.DoesNotContain(accounts, c => c.Id == account.Id);
        }

        #endregion

    }
}
