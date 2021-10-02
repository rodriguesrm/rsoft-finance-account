using AutoFixture;
using AutoFixture.Kernel;
using NUnit.Framework;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Account.Core.Services;
using RSoft.Account.Infra;
using RSoft.Account.Infra.Providers;
using RSoft.Account.Tests.DependencyInjection;
using RSoft.Account.Tests.Extensions;
using RSoft.Finance.Contracts.Enum;
using RSoft.Lib.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionTable = RSoft.Account.Infra.Tables.Transaction;
using AccrualPeriodTable = RSoft.Account.Infra.Tables.AccrualPeriod;
using AccountDomain = RSoft.Account.Core.Entities.Entry;
using RSoft.Account.Tests.Stubs;
using RSoft.Account.Infra.Extensions;
using Microsoft.Extensions.Localization;
using RSoft.Account.Application.Arguments;

namespace RSoft.Account.Tests.Core.Services
{

    public class TransactionDomainServiceTest : TestFor<TransactionDomainService>
    {

        #region Local objects/variables

        private const float _creditValue = 9999f;
        private const float _debtValue = 1111;
        private AccountContext _dbContext;

        #endregion

        #region Constructors

        public TransactionDomainServiceTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            _fixture.WithInMemoryDatabase(out _dbContext);
            _fixture.Customizations.Add(new TypeRelay(typeof(ITransactionProvider), typeof(TransactionProvider)));
            _fixture.Customizations.Add(new TypeRelay(typeof(IAccrualPeriodProvider), typeof(AccrualPeriodProvider)));
            _fixture.Customizations.Add(new TypeRelay(typeof(IStringLocalizer<TransactionDomainService>), typeof(StringLocalizerStub<TransactionDomainService>)));
        }

        #endregion

        #region Local methods

        /// <summary>
        /// Load initial transactions
        /// </summary>
        /// <param name="transactionId">Transaction id output</param>
        private void LoadInitialTransactions(out Guid transactionId)
        {
            DateTime date = DateTime.UtcNow.AddMonths(-1);
            TransactionTable table = _dbContext.Transactions.FirstOrDefault(t => t.Year == date.Year && t.Month == date.Month);
            if (table == null)
            {
                TransactionTable rowA = _fixture.CreateTransaction(date.Year, date.Month, _creditValue, TransactionTypeEnum.Credit);
                TransactionTable rowB = _fixture.CreateTransaction(date.Year, date.Month, _debtValue, TransactionTypeEnum.Debt);
                _fixture.WithSeedData(_dbContext, new List<TransactionTable>() { rowA, rowB });
                transactionId = rowA.Id;
            }
            else
            {
                transactionId = table.Id;
            }
        }

        #endregion

        #region Tests

        [Test]
        public async Task AddAsync_ReturnEntitySaved()
        {
            DateTime date = DateTime.UtcNow.AddMonths(-1);
            Transaction transaction = new()
            {
                Date = date,
                TransactionType = TransactionTypeEnum.Credit,
                Amount = 300,
                Comment = "Transaction test",
                Entries = new AccountDomain(MockBuilder.InitialAccountId) { Name = "***" },
                PaymentMethod = new PaymentMethod(MockBuilder.InitialPaymentId) { Name = "***" }
            };
            Transaction result = await Sut.AddAsync(transaction, default);
            Assert.IsTrue(result.Valid);
            TransactionTable check = _dbContext.Transactions.Find(result.Id);
            Assert.NotNull(check);
            Assert.AreEqual(transaction.Id, check.Id);
            Assert.AreEqual(transaction.Year, check.Year);
            Assert.AreEqual(transaction.Month, check.Month);
            Assert.AreEqual(transaction.Amount, check.Amount);
            Assert.AreEqual(transaction.Comment, check.Comment);
        }

        [Test]
        public async Task GetById_ReturnEntity()
        {
            LoadInitialTransactions(out Guid transactionId);
            var table = _dbContext.Transactions.Find(transactionId);
            Transaction result = await Sut.GetByKeyAsync(transactionId, default);
            Assert.NotNull(result);
            Assert.AreEqual(table.Year, result.Year);
            Assert.AreEqual(table.Month, result.Month);
            Assert.AreEqual(table.Amount, result.Amount);
            Assert.AreEqual(table.TransactionType, result.TransactionType);
        }

        [Test]
        public async Task GetAllTransaction_ReturnEntityList()
        {
            DateTime date = DateTime.UtcNow.AddMonths(-1);
            LoadInitialTransactions(out Guid id);
            IEnumerable<Transaction> result = await Sut.GetAllAsync(default);
            Assert.GreaterOrEqual(result.Count(), 2);
            var transactionA = _dbContext.Transactions.First(t => t.Year == date.Year && t.Month == date.Month && t.Amount == _creditValue && t.TransactionType == TransactionTypeEnum.Credit);
            var transactionB = _dbContext.Transactions.First(t => t.Year == date.Year && t.Month == date.Month && t.Amount == _debtValue && t.TransactionType == TransactionTypeEnum.Debt);
            Assert.True(result.Any(x => x.Id == transactionA.Id));
            Assert.True(result.Any(x => x.Id == transactionB.Id));
        }

        [Test]
        public void UpdateTransaction_SuccessOnUpdate()
        {
            DateTime date = DateTime.UtcNow.AddMonths(-1);
            float oldAmount = 77;
            float newAmount = 999;
            TransactionTable oldTableRow = _fixture.CreateTransaction(date.Year, date.Month, oldAmount, TransactionTypeEnum.Credit);
            _fixture.WithSeedData(_dbContext, new TransactionTable[] { oldTableRow });
            Transaction transaction = new(oldTableRow.Id)
            {
                Date = date,
                TransactionType = TransactionTypeEnum.Debt,
                Amount = newAmount,
                Comment = oldTableRow.Comment,
                Entries = new AccountDomain(oldTableRow.AccountId) { Name = "***" },
                PaymentMethod = new PaymentMethod(oldTableRow.PaymentMethodId) { Name = "***" },
                CreatedAuthor = new Author<Guid>(oldTableRow.CreatedAuthor.Id, oldTableRow.CreatedAuthor.GetFullName())
            };
            transaction = Sut.Update(transaction.Id, transaction);
            TransactionTable check = _dbContext.Transactions.FirstOrDefault(t => t.Id == transaction.Id);
            Assert.NotNull(transaction);
            Assert.NotNull(check);
            Assert.AreEqual(check.Year, transaction.Year);
            Assert.AreEqual(check.Month, transaction.Month);
            Assert.AreEqual(check.TransactionType, transaction.TransactionType);
            Assert.AreEqual(transaction.Amount, newAmount);
        }

        [Test]
        public void DeleteTransaction_SuccessOnDelete()
        {
            DateTime date = DateTime.UtcNow.AddMonths(-1);
            TransactionTable tableRow = _fixture.CreateTransaction(date.Year, date.Month, 777, TransactionTypeEnum.Credit);
            Guid transactionId = tableRow.Id;
            _fixture.WithSeedData(_dbContext, new TransactionTable[] { tableRow });
            Sut.Delete(transactionId);
            _dbContext.SaveChanges();
            TransactionTable check = _dbContext.Transactions.FirstOrDefault(c => c.Id == transactionId);
            Assert.Null(check);
        }

        [Test]
        public void ValidateTransaction_WhenAccrualPeriodNotExists_SetNotificationsInEntity()
        {
            DateTime date = DateTime.UtcNow.AddYears(5);
            LoadInitialTransactions(out _);
            Transaction transaction = _fixture.CreateTransaction(date.Year, date.Month, 100.0f, TransactionTypeEnum.Debt).Map();
            Sut.ValidateAccrualPeriod(transaction);
            Assert.AreEqual(1, transaction.Notifications.Count);
            Assert.True(transaction.Notifications.Any(n => n.Message == "ACCRUAL_PERIOD_NOT_FOUND"));
        }

        [Test]
        public void ValidateTransaction_WhenAccrualPeriodIsClosed_SetNotificationsInEntity()
        {
            DateTime date = DateTime.UtcNow.AddYears(5);
            LoadInitialTransactions(out _);

            AccrualPeriodTable accrualPeriod = _fixture.CreateAccrualPeriod(date.Year, date.Month, 12000);

            accrualPeriod.IsClosed = true;
            accrualPeriod.UserIdClosed = AuthenticatedUserStub.UserAdminId;
            accrualPeriod.TotalCredits = 10000;
            accrualPeriod.TotalDebts = 7000;

            _dbContext.AccrualPeriods.Add(accrualPeriod);
            _dbContext.SaveChanges();

            Transaction transaction = _fixture.CreateTransaction(date.Year, date.Month, 100.0f, TransactionTypeEnum.Debt).Map();
            Sut.ValidateAccrualPeriod(transaction);
            Assert.AreEqual(1, transaction.Notifications.Count);
            Assert.True(transaction.Notifications.Any(n => n.Message == "ACCRUAL_PERIOD_IS_CLOSED"));

            _dbContext.Remove(accrualPeriod);
            _dbContext.SaveChanges();

        }

        [Test]
        public void ValidateTransaction_WhenFilterArgsIsInvalid_ThrowException()
        {
            DateTime date = DateTime.UtcNow.AddMonths(10);
            date = new DateTime(date.Year, date.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            async Task GetByFilter()
            {
                IListTransactionFilter filter = new ListTransactionFilter()
                {
                    Year = date.Year,
                    Month = date.Month,
                    StartAt = date,
                    EndAt = date.AddMonths(1).AddDays(-1)
                };
                _ = await Sut.GetByFilterAsync(filter, default);
            }
            Assert.ThrowsAsync<ArgumentException>(GetByFilter);
        }

        [Test]
        public void ValidateTransaction_WhenDatePeriodFilterArgsIsValid_ReturnListResult()
        {
            DateTime date = DateTime.UtcNow.AddMonths(-1);
            DateTime startAt = new(date.Year, date.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime endAt = startAt.AddMonths(1).AddDays(-1);
            LoadInitialTransactions(out Guid transactionId);
            IListTransactionFilter filter = new ListTransactionFilter()
            {
                StartAt = startAt,
                EndAt = endAt
            };
            IEnumerable<Transaction> result = Sut.GetByFilterAsync(filter, default).Result;

            Assert.NotNull(result);
            Assert.GreaterOrEqual(2, result.Count());
            Assert.True(result.Any(t => t.Id == transactionId));
        }

        [Test]
        public void ValidateTransaction_WhenAccrualPeriodFilterArgsIsValid_ReturnListResult()
        {
            DateTime date = DateTime.UtcNow.AddMonths(-1);
            LoadInitialTransactions(out Guid transactionId);
            IListTransactionFilter filter = new ListTransactionFilter()
            {
                Year = date.Year,
                Month = date.Month,
                AccountId = MockBuilder.InitialAccountId,
                PaymentMethodId = MockBuilder.InitialPaymentId,
                TransactionType = TransactionTypeEnum.Credit
            };
            IEnumerable<Transaction> result = Sut.GetByFilterAsync(filter, default).Result;

            Assert.NotNull(result);
            Assert.GreaterOrEqual(1, result.Count());
            Assert.True(result.Any(t => t.Id == transactionId));
        }

        #endregion

    }
}
