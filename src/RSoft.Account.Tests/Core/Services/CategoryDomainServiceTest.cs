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
using CategoryDomain = RSoft.Account.Core.Entities.Category;

namespace RSoft.Account.Tests.Core.Services
{

    public class CategoryDomainServiceTest : TestBase
    {

        #region Local Objects/Variables

        IAuthenticatedUser _authenticatedUser = new AuthenticatedUserStub();

        #endregion

        #region Constructors

        public CategoryDomainServiceTest() : base()
        {

            
        }

        #endregion

        #region Constructors

        [Fact]
        public void AddAsync_ReturnEntitySaved()
        {
            Mock<ICategoryProvider> provider = new();
            CategoryDomain category = One<CategoryDomain>();
            provider
                .Setup(d => d.AddAsync(It.IsAny<CategoryDomain>(), It.IsAny<CancellationToken>()).Result)
                .Returns(category);
            CategoryDomainService domainService = new(provider.Object, _authenticatedUser);
            CategoryDomain result = domainService.AddAsync(category, default).Result;
            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void Update_ReturnEntityUpdated()
        {
            Mock<ICategoryProvider> provider = new();
            CategoryDomain category = One<CategoryDomain>();
            provider
                .Setup(d => d.Update(It.IsAny<Guid>(), It.IsAny<CategoryDomain>()))
                .Returns(category);
            CategoryDomainService domainService = new(provider.Object, _authenticatedUser);
            CategoryDomain result = domainService.Update(category.Id, category);
            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void GetCategoryById_ReturnCategory()
        {
            Mock<ICategoryProvider> provider = new();
            CategoryDomain category = One<CategoryDomain>();
            provider
                .Setup(d => d.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()).Result)
                .Returns(category);
            CategoryDomainService domainService = new(provider.Object, _authenticatedUser);
            CategoryDomain result = domainService.GetByKeyAsync(category.Id, default).Result;
            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void ListCategory_ReturnAllCategories()
        {
            Mock<ICategoryProvider> provider = new();
            IEnumerable<CategoryDomain> categories = new List<CategoryDomain>()
            {
                One<CategoryDomain>(), One<CategoryDomain>(), One<CategoryDomain>()
            };
            IEnumerable<Guid> ids = categories.Select(s => s.Id).ToList();
            provider
                .Setup(d => d.GetAllAsync(It.IsAny<CancellationToken>()).Result)
                .Returns(categories);
            CategoryDomainService domainService = new(provider.Object, _authenticatedUser);
            IEnumerable<CategoryDomain> result = domainService.GetAllAsync(default).Result;
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            foreach (Guid id in ids)
            {
                Assert.Contains(result, c => c.Id == id);
            }
        }

        [Fact]
        public void DeleteCategory_SucessOnDeleteAndReturnNullWhenGetRemovedEntity()
        {
            Mock<ICategoryProvider> provider = new();
            IList<CategoryDomain> categories = new List<CategoryDomain>()
            {
                One<CategoryDomain>(), One<CategoryDomain>(), One<CategoryDomain>()
            };
            CategoryDomain category = categories.First();
            provider
                .Setup(d => d.Delete(It.IsAny<Guid>()))
                .Callback<Guid>(id => 
                {
                    CategoryDomain category = categories.Where(x => x.Id == id).FirstOrDefault();
                    if (category != null)
                        categories.Remove(category);
                });
            CategoryDomainService domainService = new(provider.Object, _authenticatedUser);
            domainService.Delete(category.Id);
            Assert.Equal(2, categories.Count());
            Assert.DoesNotContain(categories, c => c.Id == category.Id);
        }

        #endregion

    }
}
