using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
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
using TransactionDomain = RSoft.Account.Core.Entities.Transaction;
using AccrualPeriodDomain = RSoft.Account.Core.Entities.AccrualPeriod;
using RSoft.Lib.Common.Abstractions;

namespace RSoft.Account.Tests.Core.Services
{

    public class TransactionDomainServiceTest : TestBase
    {

        #region Local Objects/Variables

        IAuthenticatedUser _authenticatedUser = new AuthenticatedUserStub();

        #endregion

        #region Constructors

        public TransactionDomainServiceTest() : base()
        {

            
        }

        #endregion

        #region Constructors

        [Fact]
        public void AddAsync_ReturnEntitySaved()
        {
            Mock<ITransactionProvider> provider = new();
            Mock<IAccrualPeriodProvider> accrualPeriodProvider = new();
            IStringLocalizer<TransactionDomainService> stringLocalizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<TransactionDomainService>>();
            TransactionDomain transaction = One<TransactionDomain>();
            transaction.Date = DateTime.UtcNow;
            provider
                .Setup(d => d.AddAsync(It.IsAny<TransactionDomain>(), It.IsAny<CancellationToken>()).Result)
                .Returns(transaction);
            AccrualPeriodDomain accrualPeriod = One<AccrualPeriodDomain>();
            accrualPeriod.Year = transaction.Year;
            accrualPeriod.Month = transaction.Month;
            accrualPeriodProvider
                .Setup(d => d.GetByKeyAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()).Result)
                .Returns(accrualPeriod);
            TransactionDomainService domainService = new(provider.Object, _authenticatedUser, stringLocalizer, accrualPeriodProvider.Object);
            TransactionDomain result = domainService.AddAsync(transaction, default).Result;
            Assert.NotNull(result);
            Assert.Equal(transaction.Id, result.Id);
            Assert.Equal(transaction.Amount, result.Amount);
            Assert.Equal(transaction.Comment, result.Comment);
            Assert.True(result.Valid);
        }

        [Fact]
        public void Update_ReturnEntityUpdated()
        {
            Mock<ITransactionProvider> provider = new();
            Mock<IAccrualPeriodProvider> accrualPeriodProvider = new();
            IStringLocalizer<TransactionDomainService> stringLocalizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<TransactionDomainService>>();
            TransactionDomain transaction = One<TransactionDomain>();
            provider
                .Setup(d => d.Update(It.IsAny<Guid>(), It.IsAny<TransactionDomain>()))
                .Returns(transaction);
            TransactionDomainService domainService = new(provider.Object, _authenticatedUser, stringLocalizer, accrualPeriodProvider.Object);
            TransactionDomain result = domainService.Update(transaction.Id, transaction);
            Assert.NotNull(result);
            Assert.Equal(transaction.Id, result.Id);
            Assert.Equal(transaction.Amount, result.Amount);
            Assert.Equal(transaction.Comment, result.Comment);
            Assert.True(result.Valid);
        }

        [Fact]
        public void GetTransactionById_ReturnTransaction()
        {
            Mock<ITransactionProvider> provider = new();
            Mock<IAccrualPeriodProvider> accrualPeriodProvider = new();
            IStringLocalizer<TransactionDomainService> stringLocalizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<TransactionDomainService>>();
            TransactionDomain transaction = One<TransactionDomain>();
            provider
                .Setup(d => d.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()).Result)
                .Returns(transaction);
            TransactionDomainService domainService = new(provider.Object, _authenticatedUser, stringLocalizer, accrualPeriodProvider.Object);
            TransactionDomain result = domainService.GetByKeyAsync(transaction.Id, default).Result;
            Assert.NotNull(result);
            Assert.Equal(transaction.Id, result.Id);
            Assert.Equal(transaction.Amount, result.Amount);
            Assert.Equal(transaction.Comment, result.Comment);
            Assert.True(result.Valid);
        }

        [Fact]
        public void ListTransaction_ReturnAllTransactions()
        {
            Mock<ITransactionProvider> provider = new();
            Mock<IAccrualPeriodProvider> accrualPeriodProvider = new();
            IStringLocalizer<TransactionDomainService> stringLocalizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<TransactionDomainService>>();
            IEnumerable<TransactionDomain> transactions = new List<TransactionDomain>()
            {
                One<TransactionDomain>(), One<TransactionDomain>(), One<TransactionDomain>()
            };
            IEnumerable<Guid> ids = transactions.Select(s => s.Id).ToList();
            provider
                .Setup(d => d.GetAllAsync(It.IsAny<CancellationToken>()).Result)
                .Returns(transactions);
            TransactionDomainService domainService = new(provider.Object, _authenticatedUser, stringLocalizer, accrualPeriodProvider.Object);
            IEnumerable<TransactionDomain> result = domainService.GetAllAsync(default).Result;
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            foreach (Guid id in ids)
            {
                Assert.Contains(result, c => c.Id == id);
            }
        }

        [Fact]
        public void DeleteTransaction_SucessOnDeleteAndReturnNullWhenGetRemovedEntity()
        {
            Mock<ITransactionProvider> provider = new();
            Mock<IAccrualPeriodProvider> accrualPeriodProvider = new();
            IStringLocalizer<TransactionDomainService> stringLocalizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<TransactionDomainService>>();
            IList<TransactionDomain> transactions = new List<TransactionDomain>()
            {
                One<TransactionDomain>(), One<TransactionDomain>(), One<TransactionDomain>()
            };
            TransactionDomain transaction = transactions.First();
            provider
                .Setup(d => d.Delete(It.IsAny<Guid>()))
                .Callback<Guid>(id => 
                {
                    TransactionDomain transaction = transactions.Where(x => x.Id == id).FirstOrDefault();
                    if (transaction != null)
                        transactions.Remove(transaction);
                });
            TransactionDomainService domainService = new(provider.Object, _authenticatedUser, stringLocalizer, accrualPeriodProvider.Object);
            domainService.Delete(transaction.Id);
            Assert.Equal(2, transactions.Count());
            Assert.DoesNotContain(transactions, c => c.Id == transaction.Id);
        }

        #endregion

    }
}
