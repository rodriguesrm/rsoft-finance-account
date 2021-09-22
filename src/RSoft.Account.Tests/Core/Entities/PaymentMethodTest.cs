using System;
using Xunit;
using RSoft.Account.Test.DependencyInjection;
using RSoft.Account.Core.Entities;
using RSoft.Finance.Contracts.Enum;
using System.Linq;

namespace RSoft.Account.Test.Core.Entities
{

    /// <summary>
    /// PaymentMethod entity tests
    /// </summary>
    public class PaymentMethodTest
    {

        #region Constructors

        public PaymentMethodTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Fact]
        public void CreatePaymentMethodInstance_ResultSuccess()
        {

            PaymentMethod PaymentMethod1 = new();
            PaymentMethod PaymentMethod2 = new(Guid.NewGuid());
            PaymentMethod PaymentMethod3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(PaymentMethod1);
            Assert.NotNull(PaymentMethod2);
            Assert.NotNull(PaymentMethod3);

        }

        [Fact]
        public void ValidatePaymentMethodWhenDataIsInvalid_ResultInvalidTrue()
        {
            PaymentMethod PaymentMethod = new();
            PaymentMethod.Validate();
            Assert.True(PaymentMethod.Invalid);
            Assert.Equal(2, PaymentMethod.Notifications.Count);
            Assert.Contains(PaymentMethod.Notifications, n => n.Message == "FIELD_REQUIRED");
            string[] distinctFields =  PaymentMethod.Notifications.Select(n => n.Property).Distinct().ToArray();
            int qtyDistinctNotifications = PaymentMethod.Notifications.Select(n => n.Message).Distinct().Count();
            Assert.Equal(2, distinctFields.Length);
            Assert.Equal(1, qtyDistinctNotifications);
        }

        [Fact]
        public void ValidatePaymentMethodWhenDataIsValid_ResultValidTrue()
        {
            PaymentMethod PaymentMethod = new()
            {
                Name = "PaymentMethodName",
                PaymentType = PaymentTypeEnum.Money
            };
            PaymentMethod.Validate();
            Assert.True(PaymentMethod.Valid);
        }

        #endregion

    }
}
