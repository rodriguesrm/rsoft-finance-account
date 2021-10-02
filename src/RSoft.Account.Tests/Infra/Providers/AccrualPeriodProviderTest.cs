using AutoFixture;
using AutoFixture.Kernel;
using NUnit.Framework;
using RSoft.Entry.Infra;
using RSoft.Entry.Infra.Providers;
using RSoft.Entry.Infra.Tables;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Entry.Tests.Extensions;
using RSoft.Lib.Design.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AccrualPeriodDomain = RSoft.Entry.Core.Entities.AccrualPeriod;

namespace RSoft.Entry.Tests.Infra.Providers
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
