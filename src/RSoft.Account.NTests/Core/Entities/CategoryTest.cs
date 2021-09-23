using NUnit.Framework;
using RSoft.Account.Core.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RSoft.Account.NTests.Core.Entities
{

    [ExcludeFromCodeCoverage(Justification = "Test class should not be considered in test coverage.")]
    public class CategoryTest : TestFor<Category>
    {

        [Test]
        public void CreateCategoryInstance_ResultSuccess()
        {

            Category category1 = new();
            Category category2 = new(Guid.NewGuid());
            Category category3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(category1);
            Assert.NotNull(category2);
            Assert.NotNull(category3);

        }

        [Test]
        public void ValidateCategoryWhenDataIsInvalid_ResultInvalidTrue()
        {
            Category category = new();
            category.Validate();
            Assert.True(category.Invalid);
            Assert.AreEqual(1, category.Notifications.Count);
            Assert.True(category.Notifications.Any(n => n.Property == nameof(Category.Name)));
        }

        [Test]
        public void ValidateCategoryWhenDataIsValid_ResultValidTrue()
        {
            Category Category = new()
            {
                Name = "CategoryName"
            };
            Category.Validate();
            Assert.True(Category.Valid);
        }


    }
}
