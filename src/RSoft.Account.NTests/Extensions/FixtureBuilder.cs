using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RSoft.Account.Infra;
using RSoft.Account.NTests.Stubs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CategoryTable = RSoft.Account.Infra.Tables.Category;
using UserTable = RSoft.Account.Infra.Tables.User;

namespace RSoft.Account.NTests.Extensions
{

    /// <summary>
    /// Fixture and mock bilders extensions
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Test class should not be considered in test coverage.")]
    public static class FixtureBuilder
    {

        #region Local objects/variables

        private static IEnumerable<UserTable> _users;

        #endregion

        #region Local methods

        /// <summary>
        /// Prepara essential user rows
        /// </summary>
        private static void PrepareUsersRows(AccountContext context = null)
        {
            UserTable userAdmin = _users?.FirstOrDefault(u => u.FirstName == "Account");
            if (userAdmin == null)
            {
                userAdmin = new(AuthenticatedUserStub.UserAdminId) { FirstName = "Admin", LastName = "RSoft" };
                _users = new List<UserTable>() { userAdmin };
                context?.Users.Add(userAdmin);
                context?.SaveChanges();
            }
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
            PrepareUsersRows(dbContext);
            fixture.Inject(dbContext);
            return fixture;
        }


        /// <summary>
        /// Populate database memery with records passed
        /// </summary>
        /// <param name="fixture">Fixture object</param>
        /// <param name="dbContext">DbContext object</param>
        /// <param name="categories">Categories list to add in DbContext</param>
        public static IFixture WithSeedData(this IFixture fixture, AccountContext dbContext, IEnumerable<CategoryTable> categories)
        {
            dbContext.Categories.AddRange(categories);
            dbContext.SaveChanges();
            return fixture;
        }

        #endregion

    }
}
