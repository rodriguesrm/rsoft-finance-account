using AutoFixture;
using NUnit.Framework;
using RSoft.Account.Core.Entities;
using RSoft.Lib.Common.ValueObjects;
using System;
using System.Linq;

namespace RSoft.Account.NTests.Core.Entities
{

    public class UserTest : TestFor<User>
    {

        [Test]
        public void CreateUserInstance_ResultSuccess()
        {

            User user1 = new();
            User user2 = new(Guid.NewGuid());
            User user3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(user1);
            Assert.NotNull(user2);
            Assert.NotNull(user3);

        }

        [Test]
        public void ValidateUserWhenDataIsInvalid_ResultInvalidTrue()
        {
            User user = new();
            user.Validate();
            Assert.True(user.Invalid);
            Assert.AreEqual(2, user.Notifications.Count);
            Assert.True(user.Notifications.Any(n => n.Property == "First name"));
            Assert.True(user.Notifications.Any(n => n.Property == "Last name"));
        }

        [Test]
        public void ValidateUserWhenDataIsValid_ResultValidTrue()
        {
            User user = _fixture
                .Build<User>()
                .With(x => x.Name, new Name("Name", "Test"))
                .Create();
            user.Validate();
            Assert.True(user.Valid);
        }

    }
}
