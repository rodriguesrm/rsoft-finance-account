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
using PaymentMethodDomain = RSoft.Account.Core.Entities.PaymentMethod;

namespace RSoft.Account.Tests.Core.Services
{

    public class PaymentMethodDomainServiceTest : TestBase
    {

        #region Local Objects/Variables

        IAuthenticatedUser _authenticatedUser = new AuthenticatedUserStub();

        #endregion

        #region Constructors

        public PaymentMethodDomainServiceTest() : base()
        {

            
        }

        #endregion

        #region Constructors

        [Fact]
        public void AddAsync_ReturnEntitySaved()
        {
            Mock<IPaymentMethodProvider> provider = new();
            PaymentMethodDomain paymentMethod = One<PaymentMethodDomain>();
            provider
                .Setup(d => d.AddAsync(It.IsAny<PaymentMethodDomain>(), It.IsAny<CancellationToken>()).Result)
                .Returns(paymentMethod);
            PaymentMethodDomainService domainService = new(provider.Object, _authenticatedUser);
            PaymentMethodDomain result = domainService.AddAsync(paymentMethod, default).Result;
            Assert.NotNull(result);
            Assert.Equal(paymentMethod.Id, result.Id);
            Assert.Equal(paymentMethod.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void Update_ReturnEntityUpdated()
        {
            Mock<IPaymentMethodProvider> provider = new();
            PaymentMethodDomain paymentMethod = One<PaymentMethodDomain>();
            provider
                .Setup(d => d.Update(It.IsAny<Guid>(), It.IsAny<PaymentMethodDomain>()))
                .Returns(paymentMethod);
            PaymentMethodDomainService domainService = new(provider.Object, _authenticatedUser);
            PaymentMethodDomain result = domainService.Update(paymentMethod.Id, paymentMethod);
            Assert.NotNull(result);
            Assert.Equal(paymentMethod.Id, result.Id);
            Assert.Equal(paymentMethod.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void GetPaymentMethodById_ReturnPaymentMethod()
        {
            Mock<IPaymentMethodProvider> provider = new();
            PaymentMethodDomain paymentMethod = One<PaymentMethodDomain>();
            provider
                .Setup(d => d.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()).Result)
                .Returns(paymentMethod);
            PaymentMethodDomainService domainService = new(provider.Object, _authenticatedUser);
            PaymentMethodDomain result = domainService.GetByKeyAsync(paymentMethod.Id, default).Result;
            Assert.NotNull(result);
            Assert.Equal(paymentMethod.Id, result.Id);
            Assert.Equal(paymentMethod.Name, result.Name);
            Assert.True(result.Valid);
        }

        [Fact]
        public void ListPaymentMethod_ReturnAllPaymentMethods()
        {
            Mock<IPaymentMethodProvider> provider = new();
            IEnumerable<PaymentMethodDomain> payments = new List<PaymentMethodDomain>()
            {
                One<PaymentMethodDomain>(), One<PaymentMethodDomain>(), One<PaymentMethodDomain>()
            };
            IEnumerable<Guid> ids = payments.Select(s => s.Id).ToList();
            provider
                .Setup(d => d.GetAllAsync(It.IsAny<CancellationToken>()).Result)
                .Returns(payments);
            PaymentMethodDomainService domainService = new(provider.Object, _authenticatedUser);
            IEnumerable<PaymentMethodDomain> result = domainService.GetAllAsync(default).Result;
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            foreach (Guid id in ids)
            {
                Assert.Contains(result, c => c.Id == id);
            }
        }

        [Fact]
        public void DeletePaymentMethod_SucessOnDeleteAndReturnNullWhenGetRemovedEntity()
        {
            Mock<IPaymentMethodProvider> provider = new();
            IList<PaymentMethodDomain> payments = new List<PaymentMethodDomain>()
            {
                One<PaymentMethodDomain>(), One<PaymentMethodDomain>(), One<PaymentMethodDomain>()
            };
            PaymentMethodDomain paymentMethod = payments.First();
            provider
                .Setup(d => d.Delete(It.IsAny<Guid>()))
                .Callback<Guid>(id => 
                {
                    PaymentMethodDomain paymentMethod = payments.Where(x => x.Id == id).FirstOrDefault();
                    if (paymentMethod != null)
                        payments.Remove(paymentMethod);
                });
            PaymentMethodDomainService domainService = new(provider.Object, _authenticatedUser);
            domainService.Delete(paymentMethod.Id);
            Assert.Equal(2, payments.Count());
            Assert.DoesNotContain(payments, c => c.Id == paymentMethod.Id);
        }

        #endregion

    }
}
