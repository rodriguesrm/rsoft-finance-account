using AutoFixture;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.NTests
{

    /// <summary>
    /// Abstract class to test objects
    /// </summary>
    /// <typeparam name="TSut">Object type to test</typeparam>
    [ExcludeFromCodeCoverage(Justification = "Test class should not be considered in test coverage.")]
    public abstract class TestFor<TSut> : TestBase
        where TSut : class
    {

        #region Local objects/variables

        private Lazy<TSut> _lazySut;

        #endregion

        #region Properties

        /// <summary>
        /// Object instance to be tested
        /// </summary>
        protected TSut Sut => _lazySut.Value;

        #endregion

        #region Local methods

        /// <summary>
        /// Create a instance for object to test
        /// </summary>
        protected virtual TSut CreateSut()
            => _fixture.Create<TSut>();

        /// <summary>
        /// Setup test
        /// </summary>
        /// <param name="fixture">Fixture object instance</param>
        protected virtual void Setup(IFixture fixture)
        {
        }

        #endregion

        #region Public methods

        [SetUp]
        public void SetUpTestsFor()
        {
            _lazySut = new Lazy<TSut>(CreateSut);
            Setup(_fixture);
        }

        #endregion

    }
}
