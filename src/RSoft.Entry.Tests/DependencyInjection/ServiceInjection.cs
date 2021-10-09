using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using RSoft.Lib.Common.Contracts;
using RSoft.Lib.Common.Abstractions;
using RSoft.Finance.Contracts.Enum;
using CategoryDomain = RSoft.Entry.Core.Entities.Category;
using EntryDomain = RSoft.Entry.Core.Entities.Entry;
using AccrualPeriodDomain = RSoft.Entry.Core.Entities.AccrualPeriod;
using TransactionDomain = RSoft.Entry.Core.Entities.Transaction;
using UserDomain = RSoft.Entry.Core.Entities.User;
using RSoft.Entry.Core.Services;
using RSoft.Entry.Tests.Stubs;
using RSoft.Lib.Common.Contracts.Web;
using Microsoft.Extensions.Logging;
using RSoft.Entry.WorkerService.Consumers;

namespace RSoft.Entry.Tests.DependencyInjection
{

    /// <summary>
    /// Services injection static class
    /// </summary>
    public static class ServiceInjection
    {

        #region Local Objects/Variables


        private static IServiceCollection _serviceCollection = null;
        private static IServiceProvider _serviceProvider = null;

        #endregion

        #region Properties

        /// <summary>
        /// Get service collection
        /// </summary>
        public static IServiceCollection ServiceCollection
        {
            get
            {
                if (_serviceCollection == null)
                {
                    _serviceCollection = new ServiceCollection()

                        .AddScoped<IStringLocalizer<CategoryDomain>, StringLocalizerStub<CategoryDomain>>()
                        .AddScoped<IStringLocalizer<EntryDomain>, StringLocalizerStub<EntryDomain>>()
                        .AddScoped<IStringLocalizer<SimpleStringValidationContract>, StringLocalizerStub<SimpleStringValidationContract>>()
                        .AddScoped<IStringLocalizer<RequiredValidationContract<Guid?>>, StringLocalizerStub<RequiredValidationContract<Guid?>>>()
                        .AddScoped<IStringLocalizer<AccrualPeriodDomain>, StringLocalizerStub<AccrualPeriodDomain>>()
                        .AddScoped<IStringLocalizer<EnumCastFromIntegerValidationContract<PaymentTypeEnum>>, StringLocalizerStub<EnumCastFromIntegerValidationContract<PaymentTypeEnum>>>()
                        .AddScoped<IStringLocalizer<TransactionDomain>, StringLocalizerStub<TransactionDomain>>()
                        .AddScoped<IStringLocalizer<PastDateValidationContract>, StringLocalizerStub<PastDateValidationContract>>()
                        .AddScoped<IStringLocalizer<EnumCastFromIntegerValidationContract<TransactionTypeEnum>>, StringLocalizerStub<EnumCastFromIntegerValidationContract<TransactionTypeEnum>>>()
                        .AddScoped<IStringLocalizer<UserDomain>, StringLocalizerStub<UserDomain>>()
                        .AddScoped<IStringLocalizer<FullNameValidationContract>, StringLocalizerStub<FullNameValidationContract>>()
                        .AddScoped<IStringLocalizer<TransactionDomainService>, StringLocalizerStub<TransactionDomainService>>()
                        .AddScoped<IAuthenticatedUser, AuthenticatedUserStub>()
                        .AddScoped<MediatR.IMediator, MediatorSub>()
                        .AddSingleton<ILogger<AccrualPeriodStartedEventConsumer>, LoggerStub<AccrualPeriodStartedEventConsumer>>()
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

        /// <summary>
        /// Build provider for service activator
        /// </summary>
        public static IServiceProvider BuildProvider()
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = ServiceCollection.BuildServiceProvider();
                ServiceActivator.Configure(GetServiceProvider);
            }
            return _serviceProvider;
        }

        #endregion

    }
}
