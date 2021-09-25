using NUnit.Framework;
using RSoft.Account.Core.Entities;
using RSoft.Finance.Contracts.Enum;
using RSoft.Lib.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Account.NTests.Core.Entities
{

    public class PaymentMethodTest : TestFor<PaymentMethod>
    {

        #region Tests

        [Test]
        public void CreatePaymentMethodInstance_ResultSuccess()
        {

            PaymentMethod paymentMethod1 = new();
            PaymentMethod paymentMethod2 = new(Guid.NewGuid());
            PaymentMethod paymentMethod3 = new("1d899c45-6d83-41d4-afdc-e004a6250559");

            Assert.NotNull(paymentMethod1);
            Assert.NotNull(paymentMethod2);
            Assert.NotNull(paymentMethod3);

        }

        [Test]
        public void ValidatePaymentMethodWhenDataIsInvalid_ResultInvalidTrue()
        {
            PaymentMethod paymentMethod = new();
            paymentMethod.Validate();
            Assert.True(paymentMethod.Invalid);
            Assert.AreEqual(2, paymentMethod.Notifications.Count);
            Assert.True(paymentMethod.Notifications.Any(n => n.Property == nameof(PaymentMethod.Name)));
            Assert.True(paymentMethod.Notifications.Any(n => n.Property == "paymentType"));
            string[] distinctFields = paymentMethod.Notifications.Select(n => n.Property).Distinct().ToArray();
            int qtyDistinctNotifications = paymentMethod.Notifications.Select(n => n.Message).Distinct().Count();
            Assert.AreEqual(2, distinctFields.Length);
            Assert.AreEqual(1, qtyDistinctNotifications);
        }

        [Test]
        public void ValidatePaymentMethodWhenDataIsValid_ResultValidTrue()
        {
            PaymentMethod PaymentMethod = new()
            {
                Name = "PaymentMethodName",
                PaymentType = PaymentTypeEnum.Money,
                CreatedAuthor = One<Author<Guid>>(),
                ChangedAuthor = One<AuthorNullable<Guid>>()
            };
            PaymentMethod.Validate();
            Assert.True(PaymentMethod.Valid);
        }

        #endregion

    }
}
