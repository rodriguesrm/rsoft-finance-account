using AutoFixture;
using AutoFixture.Kernel;
using NUnit.Framework;
using RSoft.Account.Core.Ports;
using RSoft.Account.Core.Services;
using RSoft.Account.Infra;
using RSoft.Account.Infra.Providers;
using RSoft.Account.Tests.DependencyInjection;
using RSoft.Account.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountTable = RSoft.Account.Infra.Tables.Account;
using AccountDomain = RSoft.Account.Core.Entities.Account;
using CategoryDomain = RSoft.Account.Core.Entities.Category;

namespace RSoft.Account.Tests.Core.Services
{

    public class AccountDomainServiceTest : TestFor<AccountDomainService>
    {

        #region Local objects/variables

        private const string _accountAName = "SUPERMARKET";
        private const string _accountBName = "TRAVEL";

        private AccountContext _dbContext;

        #endregion

        #region Constructors

        public AccountDomainServiceTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            _fixture.WithInMemoryDatabase(out _dbContext);
            _fixture.Customizations.Add(new TypeRelay(typeof(IAccountProvider), typeof(AccountProvider)));

        }

        #endregion

        #region Local methods

        /// <summary>
        /// Load initial categories
        /// </summary>
        /// <param name="accountId">Account id output</param>
        private void LoadInitialAccount(out Guid accountId)
        {
            AccountTable accountA = _dbContext.Accounts.FirstOrDefault(a => a.Name == _accountAName);
            if (accountA == null)
            {
                AccountTable rowA = _fixture.CreateAccount(_accountAName);
                AccountTable rowB = _fixture.CreateAccount(_accountBName);
                _fixture.WithSeedData(_dbContext, new List<AccountTable>() { rowA, rowB });
                accountId = rowA.Id;
            }
            else
            {
                accountId = accountA.Id;
            }
        }

        #endregion

        #region Tests

        [Test]
        public async Task AddAsync_ReturnEntitySaved()
        {
            AccountDomain Account = _fixture.Build<AccountDomain>()
                .With(c => c.Name, "Account TEST")
                .Create();
            AccountDomain result = await Sut.AddAsync(Account, default);
            Assert.IsTrue(result.Valid);
            AccountTable check = _dbContext.Accounts.Find(result.Id);
            Assert.NotNull(check);
            Assert.AreEqual(Account.Id, check.Id);
            Assert.AreEqual(Account.Name, check.Name);
        }

        [Test]
        public async Task GetById_ReturnEntity()
        {
            LoadInitialAccount(out Guid accountId);
            AccountTable table = _dbContext.Accounts.Find(accountId);
            AccountDomain result = await Sut.GetByKeyAsync(accountId, default);
            Assert.NotNull(result);
            Assert.AreEqual(table.Name, result.Name);
        }

        [Test]
        public async Task GetAllAccount_ReturnEntityList()
        {
            LoadInitialAccount(out _);
            IEnumerable<AccountDomain> result = await Sut.GetAllAsync(default);
            Assert.GreaterOrEqual(result.Count(), 2);
            AccountTable AccountA = _dbContext.Accounts.First(c => c.Name == _accountAName);
            AccountTable AccountB = _dbContext.Accounts.First(c => c.Name == _accountBName);
            Assert.True(result.Any(x => x.Id == AccountA.Id));
            Assert.True(result.Any(x => x.Id == AccountB.Id));
        }

        [Test]
        public void UpdateAccount_SuccessOnUpdate()
        {
            string oldName = "Account X";
            string newName = "New Account";
            AccountTable oldTableRow = _fixture.CreateAccount(oldName);
            _fixture.WithSeedData(_dbContext, new AccountTable[] { oldTableRow });
            AccountDomain account = new(oldTableRow.Id) { Name = newName, Category = new CategoryDomain(MockBuilder.InitialCategoryId) { Name = _fixture.GetItinialCategoryName() } };
            account = Sut.Update(account.Id, account);
            AccountTable check = _dbContext.Accounts.Where(c => c.Id == account.Id).FirstOrDefault();
            Assert.NotNull(account);
            Assert.NotNull(check);
            Assert.AreEqual(check.Name, account.Name);
            Assert.AreEqual(check.Name, newName);
            Assert.AreEqual(account.Name, newName);
        }

        [Test]
        public void DeleteAccount_SuccessOnDelete()
        {
            AccountTable tableRow = _fixture.CreateAccount("Account TO_REMOVE");
            Guid accountId = tableRow.Id;
            _fixture.WithSeedData(_dbContext, new AccountTable[] { tableRow });
            Sut.Delete(accountId);
            _dbContext.SaveChanges();
            AccountTable check = _dbContext.Accounts.FirstOrDefault(c => c.Id == accountId);
            Assert.Null(check);
        }

        #endregion

    }
}
