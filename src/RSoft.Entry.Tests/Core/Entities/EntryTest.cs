using NUnit.Framework;
using System;
using System.Linq;
using EntryDomain = RSoft.Entry.Core.Entities.Entry;
using CategoryDomain = RSoft.Entry.Core.Entities.Category;

namespace RSoft.Entry.Tests.Core.Entities
{

    public class EntryTest : TestFor<EntryDomain>
    {

        #region Constructors

        public EntryTest() : base() { }

        #endregion

        #region Tests

        [Test]
        public void CreateEntryInstance_ResultSuccess()
        {

            EntryDomain entity1 = new();
            EntryDomain entity2 = new(Guid.NewGuid());
            EntryDomain entyty3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(entity1);
            Assert.NotNull(entity2);
            Assert.NotNull(entyty3);

        }

        [Test]
        public void ValidateEntryWhenDataIsInvalid_ResultInvalidTrue()
        {
            EntryDomain entity = new();
            entity.Validate();
            Assert.True(entity.Invalid);
            Assert.AreEqual(2, entity.Notifications.Count);
            Assert.True(entity.Notifications.Any(n => n.Message == "FIELD_REQUIRED"));
            Assert.True(entity.Notifications.Any(n => n.Message == "CATEGORY_REQUIRED"));
        }

        [Test]
        public void ValidateEntryWhenDataIsValid_ResultValidTrue()
        {
            EntryDomain entity = One<EntryDomain>();
            entity.Category = new CategoryDomain(Guid.NewGuid()) { Name = "**" };
            entity.Name = "CategoryName";
            entity.Validate();
            Assert.True(entity.Valid);
        }

        #endregion

    }
}
