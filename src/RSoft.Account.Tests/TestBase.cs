using AutoFixture;
using RSoft.Account.Test.DependencyInjection;

namespace RSoft.Account.Tests
{

    /// <summary>
    /// Abstract class base
    /// </summary>
    public abstract class TestBase
    {

        #region Local objects/variables

        protected readonly IFixture _fixture;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new test instance
        /// </summary>
        public TestBase()
        {
            _fixture = new Fixture();
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Create a mock instance
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        protected T One<T>()
            => _fixture.Create<T>();

        #endregion

    }
}
