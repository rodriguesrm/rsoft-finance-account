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
using AccrualPeriodDomain = RSoft.Account.Core.Entities.AccrualPeriod;

namespace RSoft.Account.Tests.Core.Services
{

    public class AccrualPeriodDomainServiceTest : TestBase
    {

        #region Local Objects/Variables

        IAuthenticatedUser _authenticatedUser = new AuthenticatedUserStub();

        #endregion

        #region Constructors

        public AccrualPeriodDomainServiceTest() : base()
        {

            
        }

        #endregion

        #region Constructors

        [Fact]
        public void AddAsync_ReturnEntitySaved()
        {
            Mock<IAccrualPeriodProvider> provider = new();
            AccrualPeriodDomain accrualPeriod = One<AccrualPeriodDomain>();
            accrualPeriod.Year = DateTime.UtcNow.Year;
            accrualPeriod.Month = DateTime.UtcNow.Month;
            provider
                .Setup(d => d.AddAsync(It.IsAny<AccrualPeriodDomain>(), It.IsAny<CancellationToken>()).Result)
                .Returns(accrualPeriod);
            AccrualPeriodDomainService domainService = new(provider.Object, _authenticatedUser);
            AccrualPeriodDomain result = domainService.AddAsync(accrualPeriod, default).Result;
            Assert.NotNull(result);
            Assert.Equal(accrualPeriod.Year, result.Year);
            Assert.Equal(accrualPeriod.Month, result.Month);
            Assert.True(result.Valid);
        }

        [Fact]
        public void Update_ReturnEntityUpdated()
        {
            Mock<IAccrualPeriodProvider> provider = new();
            AccrualPeriodDomain accrualPeriod = One<AccrualPeriodDomain>();
            accrualPeriod.Year = DateTime.UtcNow.Year;
            accrualPeriod.Month = DateTime.UtcNow.Month;
            provider
                .Setup(d => d.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<AccrualPeriodDomain>()))
                .Returns(accrualPeriod);
            AccrualPeriodDomainService domainService = new(provider.Object, _authenticatedUser);
            AccrualPeriodDomain result = domainService.Update(accrualPeriod.Year, accrualPeriod.Month, accrualPeriod);
            Assert.NotNull(result);
            Assert.Equal(accrualPeriod.Year, result.Year);
            Assert.Equal(accrualPeriod.Month, result.Month);
            Assert.True(result.Valid);
        }

        [Fact]
        public void GetAccrualPeriodById_ReturnAccrualPeriod()
        {
            Mock<IAccrualPeriodProvider> provider = new();
            AccrualPeriodDomain accrualPeriod = One<AccrualPeriodDomain>();
            accrualPeriod.Year = DateTime.UtcNow.Year;
            accrualPeriod.Month = DateTime.UtcNow.Month;
            provider
                .Setup(d => d.GetByKeyAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()).Result)
                .Returns(accrualPeriod);
            AccrualPeriodDomainService domainService = new(provider.Object, _authenticatedUser);
            AccrualPeriodDomain result = domainService.GetByKeyAsync(accrualPeriod.Year, accrualPeriod.Month, default).Result;
            Assert.NotNull(result);
            Assert.Equal(accrualPeriod.Year, result.Year);
            Assert.Equal(accrualPeriod.Month, result.Month);
            Assert.True(result.Valid);
        }

        [Fact]
        public void ListAccrualPeriod_ReturnAllAccrualPeriods()
        {
            Mock<IAccrualPeriodProvider> provider = new();
            IEnumerable<AccrualPeriodDomain> accrualPeriods = new List<AccrualPeriodDomain>()
            {
                One<AccrualPeriodDomain>(), One<AccrualPeriodDomain>(), One<AccrualPeriodDomain>()
            };
            var ids = accrualPeriods.Select(s => new { s.Year, s.Month }).ToList();
            provider
                .Setup(d => d.GetAllAsync(It.IsAny<CancellationToken>()).Result)
                .Returns(accrualPeriods);
            AccrualPeriodDomainService domainService = new(provider.Object, _authenticatedUser);
            IEnumerable<AccrualPeriodDomain> result = domainService.GetAllAsync(default).Result;
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            foreach (var id in ids)
            {
                Assert.Contains(result, c => c.Year == id.Year && c.Month == id.Month);
            }
        }

        //[Fact]
        //public void DeleteAccrualPeriod_SucessOnDeleteAndReturnNullWhenGetRemovedEntity()
        //{
        //    Mock<IAccrualPeriodProvider> provider = new();
        //    IList<AccrualPeriodDomain> accrualPeriods = new List<AccrualPeriodDomain>()
        //    {
        //        One<AccrualPeriodDomain>(), One<AccrualPeriodDomain>(), One<AccrualPeriodDomain>()
        //    };
        //    AccrualPeriodDomain AccrualPeriod = accrualPeriods.First();
        //    provider
        //        .Setup(d => d.Delete(It.IsAny<Guid>()))
        //        .Callback<Guid>(id => 
        //        {
        //            AccrualPeriodDomain AccrualPeriod = accrualPeriods.Where(x => x.Id == id).FirstOrDefault();
        //            if (AccrualPeriod != null)
        //                accrualPeriods.Remove(AccrualPeriod);
        //        });
        //    AccrualPeriodDomainService domainService = new(provider.Object, _authenticatedUser);
        //    domainService.Delete(AccrualPeriod.Id);
        //    Assert.Equal(2, accrualPeriods.Count());
        //    Assert.DoesNotContain(accrualPeriods, c => c.Id == AccrualPeriod.Id);
        //}

        #endregion

    }
}
