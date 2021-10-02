using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RSoft.Entry.Infra;
using RSoft.Entry.Tests.Stubs;
using RSoft.Lib.Design.Infra.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using RSoft.Finance.Contracts.Enum;
using CategoryTable = RSoft.Entry.Infra.Tables.Category;
using UserTable = RSoft.Entry.Infra.Tables.User;
using AccountTable = RSoft.Entry.Infra.Tables.Account;
using PaymentMethodTable = RSoft.Entry.Infra.Tables.PaymentMethod;
using AccrualPeriodTable = RSoft.Entry.Infra.Tables.AccrualPeriod;
using TransactionTable = RSoft.Entry.Infra.Tables.Transaction;

namespace RSoft.Entry.Tests.Extensions
{

    /// <summary>
    /// Fixture and mock bilders extensions
    /// </summary>
    public static class MockBuilder
    {

        #region Local objects/variables

        private static Guid _initialCategoryId;
        private static string _initialCategoryName = "INITIAL CATEGORY";

        private static Guid _initialAccountId;
        private static string _initialAccountName = "INITIAL ACCOUNT";

        private static Guid _initialPaymentId;
        private static string _initialPaymentName = "INITIAL PAYMENTMETHOD";

        #endregion

        #region Properties


        public static Guid InitialCategoryId => _initialCategoryId;

        public static Guid InitialAccountId => _initialAccountId;

        public static Guid InitialPaymentId => _initialPaymentId;

        #endregion

        #region Local methods

        /// <summary>
        /// Prepara essential user rows
        /// </summary>
        private static void PrepareEssentialssRows(AccountContext context = null)
        {

            IFixture fixture = new Fixture();

            DateTime date = DateTime.UtcNow.Date.AddMonths(-1);

            UserTable userAdmin = context?.Users.FirstOrDefault(u => u.FirstName == "Account");
            if (userAdmin == null)
            {
                userAdmin = new(AuthenticatedUserStub.UserAdminId) { FirstName = "Admin", LastName = "RSoft" };
                context?.Users.Add(userAdmin);
            }

            CategoryTable category = context?.Categories.FirstOrDefault(c => c.Name == _initialCategoryName);
            if (category == null)
            {
                category = new() { Name = _initialCategoryName };
                _initialCategoryId = category.Id;
                context?.Categories.Add(category);
            }

            AccountTable account = context?.Accounts.FirstOrDefault(a => a.Name == _initialAccountName);
            if (account == null)
            {
                account = fixture.CreateAccount(_initialAccountName);
                _initialAccountId = account.Id;
                context?.Accounts.Add(account);
            }

            PaymentMethodTable payment = context?.PaymentMethods.FirstOrDefault(p => p.Name == _initialPaymentName);
            if (payment == null)
            {
                payment = fixture.CreatePaymentMethod(_initialPaymentName, PaymentTypeEnum.BankTransaction);
                _initialPaymentId = payment.Id;
                context?.PaymentMethods.Add(payment);
            }

            AccrualPeriodTable accrualPeriod = context?.AccrualPeriods.FirstOrDefault(a => a.Year == date.Year && a.Month == date.Month);
            if (accrualPeriod == null)
            {
                accrualPeriod = fixture.CreateAccrualPeriod(date.Year, date.Month, 7000);
                context.AccrualPeriods.Add(accrualPeriod);
            }

            context?.SaveChanges();

        }

        #endregion

        #region Public methods

        /// <summary>
        /// Prepara mock test object dbcontext with memory database
        /// </summary>
        /// <param name="fixture">Fixture object</param>
        /// <param name="dbContext">Database context object variable for output</param>
        public static IFixture WithInMemoryDatabase(this IFixture fixture, out AccountContext dbContext)
        {
            var options = new DbContextOptionsBuilder<AccountContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            dbContext = new AccountContext(options);
            PrepareEssentialssRows(dbContext);
            fixture.Inject(dbContext);
            return fixture;
        }


        /// <summary>
        /// Populate database memory with records passed
        /// </summary>
        /// <param name="fixture">Fixture object</param>
        /// <param name="dbContext">DbContext object</param>
        /// <param name="rows">Rows list to add in DbContext</param>
        public static IFixture WithSeedData<TTable>(this IFixture fixture, AccountContext dbContext, IEnumerable<TTable> rows)
            where TTable : class, ITable
        {
            dbContext.AddRange(rows);
            dbContext.SaveChanges();
            return fixture;
        }

        /// <summary>
        /// Get initial category name
        /// </summary>
        /// <param name="fixture">Fixture object instance</param>
        public static string GetItinialCategoryName(this IFixture fixture)
            => _initialCategoryName;

        /// <summary>
        /// Create a new mock of CategoryTable
        /// </summary>
        /// <param name="fixture">Fixture object for extends</param>
        /// <param name="categoryName">Category name</param>
        public static CategoryTable CreateCategory(this IFixture fixture, string categoryName)
            => fixture.Build<CategoryTable>()
                .With(c => c.Name, categoryName)
                .With(c => c.CreatedOn, DateTime.UtcNow.AddDays(-1))
                .With(c => c.CreatedBy, AuthenticatedUserStub.UserAdminId)
                .Without(c => c.Accounts)
                .Without(c => c.ChangedOn)
                .Without(c => c.ChangedBy)
                .Without(c => c.CreatedAuthor)
                .Without(c => c.ChangedAuthor)
                .Create();

        /// <summary>
        /// Create a new mock of Usertable
        /// </summary>
        /// <param name="fixture">Fixture object for extends</param>
        /// <param name="firstName">User first name</param>
        /// <param name="lastName">User last name</param>
        /// <returns></returns>
        public static UserTable CreateUser(this IFixture fixture, string firstName, string lastName)
            => fixture.Build<UserTable>()
                .With(u => u.FirstName, firstName)
                .With(u => u.LastName, lastName)
                .With(u => u.IsActive, true)
                .Without(u => u.CreatedCategories)
                .Without(u => u.ChangedCategories)
                .Without(u => u.CreatedPaymentMethods)
                .Without(u => u.ChangedPaymentMethods)
                .Without(u => u.CreatedAccounts)
                .Without(u => u.ChangedAccounts)
                .Without(u => u.CreatedTransactions)
                .Without(u => u.CreatedAccrualPeriods)
                .Without(u => u.ChangedAccrualPeriods)
                .Without(u => u.ClosedAccrualPeriods)
                .Create();

        /// <summary>
        /// Create a new mock of AccountTable
        /// </summary>
        /// <param name="fixture">Fixture object instance</param>
        public static AccountTable CreateAccount(this IFixture fixture, string accountName)
            => fixture.Build<AccountTable>()
                .With(a => a.Name, accountName)
                .With(a => a.IsActive, true)
                .With(a => a.CategoryId, _initialCategoryId)
                .With(a => a.CreatedOn, DateTime.UtcNow.AddDays(-1))
                .With(a => a.CreatedBy, AuthenticatedUserStub.UserAdminId)
                .Without(a => a.ChangedOn)
                .Without(a => a.ChangedBy)
                .Without(a => a.CreatedAuthor)
                .Without(a => a.ChangedAuthor)
                .Without(a => a.Category)
                .Without(a => a.Transactions)
                .Create();

        public static PaymentMethodTable CreatePaymentMethod(this IFixture fixture, string name, PaymentTypeEnum type)
            => fixture.Build<PaymentMethodTable>()
                .With(p => p.Name, name)
                .With(p => p.PaymentType, type)
                .With(p => p.IsActive, true)
                .With(p => p.CreatedOn, DateTime.UtcNow.AddDays(-1))
                .With(p => p.CreatedBy, AuthenticatedUserStub.UserAdminId)
                .Without(p => p.ChangedOn)
                .Without(p => p.ChangedBy)
                .Without(p => p.CreatedAuthor)
                .Without(p => p.ChangedAuthor)
                .Without(p => p.Transactions)
                .Create();

        public static AccrualPeriodTable CreateAccrualPeriod(this IFixture fixture, int year, int mont, float openBalance)
            => fixture.Build<AccrualPeriodTable>()
                .With(a => a.Year, year)
                .With(a => a.Month, mont)
                .With(a => a.CreatedOn, DateTime.UtcNow.AddDays(-35))
                .With(a => a.CreatedBy, AuthenticatedUserStub.UserAdminId)
                .With(a => a.OpeningBalance, openBalance)
                .With(a => a.IsClosed, false)
                .Without(a => a.UserIdClosed)
                .Without(a => a.ChangedOn)
                .Without(a => a.ChangedBy)
                .Without(a => a.CreatedAuthor)
                .Without(a => a.ChangedAuthor)
                .Without(a => a.ClosedAuthor)
                .Without(a => a.Transactions)
                .Create();

        public static TransactionTable CreateTransaction(this IFixture fixture, int year, int month, float amount, TransactionTypeEnum type = TransactionTypeEnum.Credit)
            => fixture.Build<TransactionTable>()
                .With(t => t.Year, year)
                .With(t => t.Month, month)
                .With(t => t.Date, new DateTime(year, month, DateTime.UtcNow.Day, 12, 0, 0))
                .With(t => t.Amount, amount)
                .With(t => t.TransactionType, type)
                .With(t => t.AccountId, _initialAccountId)
                .With(t => t.PaymentMethodId, _initialPaymentId)
                .With(t => t.CreatedOn, DateTime.UtcNow.AddDays(-35))
                .With(t => t.CreatedBy, AuthenticatedUserStub.UserAdminId)
                .Without(t => t.CreatedAuthor)
                .Without(t => t.AccrualPeriod)
                .Without(t => t.Account)
                .Without(t => t.PaymentMethod)
                .Create();

        #endregion

    }
}
