using System;
using Xunit;
using RSoft.Account.Test.DependencyInjection;
using RSoft.Account.Core.Entities;

namespace RSoft.Account.Test.Core.Entities
{

    /// <summary>
    /// Category entity tests
    /// </summary>
    public class CategoryTest
    {

        #region Constructors

        public CategoryTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Fact]
        public void CreateCategoryInstance_ResultSuccess()
        {

            Category category1 = new();
            Category category2 = new(Guid.NewGuid());
            Category category3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(category1);
            Assert.NotNull(category2);
            Assert.NotNull(category3);

        }

        [Fact]
        public void ValidateCategoryWhenDataIsInvalid_ResultInvalidTrue()
        {
            Category category = new();
            category.Validate();
            Assert.True(category.Invalid);
            Assert.Equal(1, category.Notifications.Count);
            Assert.Contains(category.Notifications, n => n.Message == "FIELD_REQUIRED");
        }

        [Fact]
        public void ValidateCategoryWhenDataIsValid_ResultValidTrue()
        {
            Category Category = new()
            {
                Name = "CategoryName"
            };
            Category.Validate();
            Assert.True(Category.Valid);
        }

        #endregion

    }
}
