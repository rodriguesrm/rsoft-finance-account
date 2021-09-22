using Microsoft.Extensions.DependencyInjection;
using System;

namespace RSoft.Account.Test.DependencyInjection
{

    /// <summary>
    /// Services injection static class
    /// </summary>
    public static class ServicesInjection
    {

        #region Local Objects/Variables


        private static IServiceCollection _serviceCollection = null;
        private static IServiceProvider _serviceProvider = null;

        #endregion

        #region Properties


        public static IServiceCollection ServiceCollection {
            get
            {
                if (_serviceCollection == null)
                {
                    _serviceCollection = new ServiceCollection()
                        .AddLogging();
                }
                return _serviceCollection;
            }
        }

        public static IServiceProvider GetServiceProvider
        {
            get => _serviceProvider;
        }

        #endregion

        #region Public methods

        public static IServiceProvider BuildProvider()
        {
            _serviceProvider = ServiceCollection.BuildServiceProvider();
            return _serviceProvider;
        }

        #endregion

    }
}
