using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.NTests
{

    /// <summary>
    /// Test abstract base class
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Test class should not be considered in test coverage.")]
    public abstract class TestBase
    {

        #region Local objects/variables

        protected IFixture _fixture;

        #endregion

        #region Local methods

        /// <summary>
        /// One time setup fixture
        /// </summary>
        /// <param name="fixture">Fixture object</param>
        protected virtual void OnetimeSetupFixture(IFixture fixture)
        {
        }

        /// <summary>
        /// Create a mock instance
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        protected T One<T>()
            => _fixture.Create<T>();

        #endregion

        #region Public methods

        /// <summary>
        /// Setup test
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
        }

        #endregion

    }
}
