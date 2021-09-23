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
using UserDomain = RSoft.Account.Core.Entities.User;

namespace RSoft.Account.Tests.Core.Services
{

    public class UserDomainServiceTest : TestBase
    {

        #region Local Objects/Variables

        IAuthenticatedUser _authenticatedUser = new AuthenticatedUserStub();

        #endregion

        #region Constructors

        public UserDomainServiceTest() : base()
        {

            
        }

        #endregion

        #region Constructors

        [Fact]
        public void AddAsync_ReturnEntitySaved()
        {
            Mock<IUserProvider> provider = new();
            UserDomain user = One<UserDomain>();
            provider
                .Setup(d => d.AddAsync(It.IsAny<UserDomain>(), It.IsAny<CancellationToken>()).Result)
                .Returns(user);
            UserDomainService domainService = new(provider.Object, _authenticatedUser);
            UserDomain result = domainService.AddAsync(user, default).Result;
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void Update_ReturnEntityUpdated()
        {
            Mock<IUserProvider> provider = new();
            UserDomain user = One<UserDomain>();
            provider
                .Setup(d => d.Update(It.IsAny<Guid>(), It.IsAny<UserDomain>()))
                .Returns(user);
            UserDomainService domainService = new(provider.Object, _authenticatedUser);
            UserDomain result = domainService.Update(user.Id, user);
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void GetUserById_ReturnUser()
        {
            Mock<IUserProvider> provider = new();
            UserDomain user = One<UserDomain>();
            provider
                .Setup(d => d.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()).Result)
                .Returns(user);
            UserDomainService domainService = new(provider.Object, _authenticatedUser);
            UserDomain result = domainService.GetByKeyAsync(user.Id, default).Result;
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void ListUser_ReturnAllUsers()
        {
            Mock<IUserProvider> provider = new();
            IEnumerable<UserDomain> users = new List<UserDomain>()
            {
                One<UserDomain>(), One<UserDomain>(), One<UserDomain>()
            };
            IEnumerable<Guid> ids = users.Select(s => s.Id).ToList();
            provider
                .Setup(d => d.GetAllAsync(It.IsAny<CancellationToken>()).Result)
                .Returns(users);
            UserDomainService domainService = new(provider.Object, _authenticatedUser);
            IEnumerable<UserDomain> result = domainService.GetAllAsync(default).Result;
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            foreach (Guid id in ids)
            {
                Assert.Contains(result, c => c.Id == id);
            }
        }

        [Fact]
        public void DeleteUser_SucessOnDeleteAndReturnNullWhenGetRemovedEntity()
        {
            Mock<IUserProvider> provider = new();
            IList<UserDomain> users = new List<UserDomain>()
            {
                One<UserDomain>(), One<UserDomain>(), One<UserDomain>()
            };
            UserDomain user = users.First();
            provider
                .Setup(d => d.Delete(It.IsAny<Guid>()))
                .Callback<Guid>(id => 
                {
                    UserDomain user = users.Where(x => x.Id == id).FirstOrDefault();
                    if (user != null)
                        users.Remove(user);
                });
            UserDomainService domainService = new(provider.Object, _authenticatedUser);
            domainService.Delete(user.Id);
            Assert.Equal(2, users.Count());
            Assert.DoesNotContain(users, c => c.Id == user.Id);
        }

        #endregion

    }
}
