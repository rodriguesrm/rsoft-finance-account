using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using NUnit.Framework;
using RSoft.Account.NTests.Stubs;
using RSoft.Lib.Common.Contracts.Web;

namespace RSoft.Account.NTests
{

    /// <summary>
    /// Test abstract base class
    /// </summary>
    public abstract class TestBase
    {

        #region Local objects/variables

        protected IFixture _fixture;

        #endregion

        #region Local methods

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

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Customizations.Add(new TypeRelay(typeof(IAuthenticatedUser), typeof(AuthenticatedUserStub)));

        }

        #endregion

    }
}
