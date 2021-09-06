using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Account.Core.Ports;
using RSoft.Account.Core.Services;
using RSoft.Account.Infra;
using RSoft.Account.Infra.Providers;
using RSoft.Lib.Common.Options;
using RSoft.Lib.Design.Infra.Data;
using RSoft.Lib.Design.IoC;

namespace RSoft.Account.Cross.IoC
{

    /// <summary>
    /// Dependency injection register service
    /// </summary>
    public static class DependencyInjection
    {

        /// <summary>
        /// Register dependency injection services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddAccountRegister(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddRSoftRegister<AccountContext>(configuration, true);

            #region Options

            services.Configure<CultureOptions>(options => configuration.GetSection("Application:Culture").Bind(options));

            #endregion

            #region Infra

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountProvider, AccountProvider>();
            services.AddScoped<ICategoryProvider, CategoryProvider>();
            services.AddScoped<IPaymentMethodProvider, PaymentMethodProvider>();
            services.AddScoped<ITransactionProvider, TransactionProvider>();
            services.AddScoped<IUserProvider, UserProvider>();

            #endregion

            #region Domain

            services.AddScoped<IAccountDomainService, AccountDomainService>();
            services.AddScoped<ICategoryDomainService, CategoryDomainService>();
            services.AddScoped<IPaymentMethodDomainService, PaymentMethodDomainService>();
            services.AddScoped<ITransactionDomainService, TransactionDomainService>();
            services.AddScoped<IUserDomainService, UserDomainService>();

            #endregion

            #region Application

            //services.AddScoped<IScopeAppService, ScopeAppService>();

            #endregion

            return services;

        }

    }
}
