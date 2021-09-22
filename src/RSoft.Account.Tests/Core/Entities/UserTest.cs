using System;
using Xunit;
using RSoft.Account.Test.DependencyInjection;
using RSoft.Account.Core.Entities;
using RSoft.Lib.Common.ValueObjects;

namespace RSoft.Account.Test.Core.Entities
{

    /// <summary>
    /// User entity tests
    /// </summary>
    public class UserTest
    {

        #region Constructors

        public UserTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Fact]
        public void CreateUserInstance_ResultSuccess()
        {

            User user1 = new();
            User user2 = new(Guid.NewGuid());
            User user3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(user1);
            Assert.NotNull(user2);
            Assert.NotNull(user3);

        }

        [Fact]
        public void ValidateUserWhenDataIsInvalid_ResultInvalidTrue()
        {
            User user = new()
            {
                Name = new Name(string.Empty, string.Empty)
            };
            user.Validate();
            Assert.True(user.Invalid);
            Assert.Equal(2, user.Notifications.Count);
            Assert.Contains(user.Notifications, n => n.Message == "FIRST_NAME_REQUIRED");
            Assert.Contains(user.Notifications, n => n.Message == "LAST_NAME_REQUIRED");
        }

        [Fact]
        public void ValidateUserWhenDataIsValid_ResultValidTrue()
        {
            User user = new()
            {
                Name = new Name("Name", "Test")
            };
            user.Validate();
            Assert.True(user.Valid);
        }

        #endregion

    }
}
