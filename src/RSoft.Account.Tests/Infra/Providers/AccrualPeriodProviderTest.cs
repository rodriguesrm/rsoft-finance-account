using AutoFixture;
using AutoFixture.Kernel;
using NUnit.Framework;
using RSoft.Account.Infra;
using RSoft.Account.Infra.Providers;
using RSoft.Account.Infra.Tables;
using RSoft.Account.Tests.DependencyInjection;
using RSoft.Account.Tests.Extensions;
using RSoft.Lib.Design.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AccrualPeriodDomain = RSoft.Account.Core.Entities.AccrualPeriod;

namespace RSoft.Account.Tests.Infra.Providers
{
    
    public class AccrualPeriodProviderTest : TestFor<AccrualPeriodProvider>
    {

        #region Local objects/variables

        private AccountContext _dbContext;

        #endregion

        #region Constructors

        public AccrualPeriodProviderTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            _fixture.WithInMemoryDatabase(out _dbContext);
        }

        #endregion

        #region Tests

        [Test]
        public void UpdateAccrualPeriod_WithInvalidEntity_ThrowsException()
        {
            AccrualPeriodDomain entity = One<AccrualPeriodDomain>();
            void DoUpdate()
            {
                entity.Validate();
                _ = Sut.Update(entity.Year, entity.Month, entity);
            }
            Assert.Throws<InvalidEntityException>(DoUpdate);
        }

        #endregion

    }
}
