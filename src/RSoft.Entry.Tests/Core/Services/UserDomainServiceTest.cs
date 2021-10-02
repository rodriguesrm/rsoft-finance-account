using AutoFixture;
using AutoFixture.Kernel;
using NUnit.Framework;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Core.Services;
using RSoft.Entry.Infra;
using RSoft.Entry.Infra.Providers;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Entry.Tests.Extensions;
using RSoft.Entry.Tests.Stubs;
using RSoft.Lib.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTable = RSoft.Entry.Infra.Tables.User;

namespace RSoft.Entry.Tests.Core.Services
{

    public class UserDomainServiceTest : TestFor<UserDomainService>
    {

        #region Local objects/variables

        private EntryContext _dbContext;

        #endregion

        #region Constructors

        public UserDomainServiceTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            _fixture.WithInMemoryDatabase(out _dbContext);
            _fixture.Customizations.Add(new TypeRelay(typeof(IUserProvider), typeof(UserProvider)));
        }

        #endregion

        #region Tests

        [Test]
        public async Task AddAsync_ReturnEntitySaved()
        {
            User user = _fixture.Build<User>()
                .With(c => c.Name, new Name("USER", "TEST"))
                .Create();
            User result = await Sut.AddAsync(user, default);
            Assert.IsTrue(result.Valid);
            UserTable check = _dbContext.Users.Find(result.Id);
            Assert.NotNull(check);
            Assert.AreEqual(user.Id, check.Id);
            Assert.AreEqual(user.Name.GetFullName(), check.GetFullName());
        }

        [Test]
        public async Task GetById_ReturnEntity()
        {
            Guid userId = AuthenticatedUserStub.UserAdminId;
            UserTable table = _dbContext.Users.Find(userId);
            User result = await Sut.GetByKeyAsync(userId, default);
            Assert.NotNull(result);
            Assert.AreEqual(table.FirstName, result.Name.FirstName);
            Assert.AreEqual(table.LastName, result.Name.LastName);
            Assert.AreEqual(table.IsActive, result.IsActive);
        }

        [Test]
        public async Task GetAllCategory_ReturnEntityList()
        {
            IEnumerable<User> result = await Sut.GetAllAsync(default);
            Assert.NotNull(result);
            Assert.GreaterOrEqual(result.Count(), 1);
            Assert.True(result.Any(x => x.Id == AuthenticatedUserStub.UserAdminId));
        }

        [Test]
        public void UpdateUser_SuccessOnUpdate()
        {
            Name oldName = new("PEPPER", "POTTS");
            Name newName = new("NATASHA", "ROMANOFF");
            UserTable oldUser = _fixture.CreateUser(oldName.FirstName, oldName.LastName);
            _fixture.WithSeedData(_dbContext, new UserTable[] { oldUser });
            User newUser = new(oldUser.Id) { Name = newName };
            _ = Sut.Update(newUser.Id, newUser);
            UserTable check = _dbContext.Users.Where(c => c.Id == newUser.Id).FirstOrDefault();
            Assert.NotNull(newUser);
            Assert.NotNull(check);
            Assert.AreEqual(check.FirstName, newUser.Name.FirstName);
            Assert.AreEqual(check.LastName, newUser.Name.LastName);
            Assert.AreEqual(newUser.Name.FirstName, newName.FirstName);
            Assert.AreEqual(newUser.Name.LastName, newName.LastName);
            Assert.AreEqual(check.FirstName, newName.FirstName);
            Assert.AreEqual(check.LastName, newName.LastName);
        }

        [Test]
        public void DeleteUser_SuccessOnDelete()
        {
            UserTable tableRow = _fixture.CreateUser("ALEXANDER", "PIERCE");
            Guid userId = tableRow.Id;
            _fixture.WithSeedData(_dbContext, new UserTable[] { tableRow });
            Sut.Delete(userId);
            _dbContext.SaveChanges();
            UserTable check = _dbContext.Users.FirstOrDefault(c => c.Id == userId);
            Assert.Null(check);
        }


        #endregion

    }
}
