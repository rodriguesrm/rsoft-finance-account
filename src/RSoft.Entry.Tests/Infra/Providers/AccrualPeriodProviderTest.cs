using AutoFixture;
using NUnit.Framework;
using RSoft.Entry.Infra.Providers;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Entry.Tests.Extensions;
using RSoft.Lib.Design.Exceptions;

using AccrualPeriodDomain = RSoft.Entry.Core.Entities.AccrualPeriod;

namespace RSoft.Entry.Tests.Infra.Providers
{

    public class AccrualPeriodProviderTest : TestFor<AccrualPeriodProvider>
    {

        #region Constructors

        public AccrualPeriodProviderTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            _fixture.WithInMemoryDatabase(out _);
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
