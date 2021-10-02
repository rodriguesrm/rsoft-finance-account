﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Core.Services;
using RSoft.Entry.Infra;
using RSoft.Entry.Infra.Providers;
using RSoft.Lib.Common.Options;
using RSoft.Lib.Design.Infra.Data;
using RSoft.Lib.Design.IoC;
using System;
using System.Collections.Generic;
using RSoft.Lib.Messaging.Abstractions;
using RSoft.Lib.Messaging.Options;
using RSoft.Lib.Messaging.Extensions;
using RSoft.Finance.Contracts.Events;
using RSoft.Entry.Application.Consumers;

namespace RSoft.Entry.Cross.IoC
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

            services.AddRSoftRegister<EntryContext>(configuration, true);

            #region Options

            services.Configure<CultureOptions>(options => configuration.GetSection("Application:Culture").Bind(options));
            services.Configure<MessagingOption>(options => configuration.GetSection("Messaging:Server").Bind(options));

            #endregion

            services.AddMassTransitUsingRabbitMq(configuration, cfg =>
            {
                //BACKLOG: Add retries
                //BACKLOG: Move consumer to worker
                // Events consumers
                cfg.AddEventConsumerEndpoint<AccrualPeriodStartedEvent, AccrualPeriodStartedEventConsumer>($"{nameof(AccrualPeriodStartedEvent)}.AccountService");
            });

            #region Infra

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEntryProvider, EntryProvider>();
            services.AddScoped<IAccrualPeriodProvider, AccrualPeriodProvider>();
            services.AddScoped<ICategoryProvider, CategoryProvider>();
            services.AddScoped<IPaymentMethodProvider, PaymentMethodProvider>();
            services.AddScoped<ITransactionProvider, TransactionProvider>();
            services.AddScoped<IUserProvider, UserProvider>();

            #endregion

            #region Domain

            services.AddScoped<IAccountDomainService, AccountDomainService>();
            services.AddScoped<IAccrualPeriodDomainService, AccrualPeriodDomainService>();
            services.AddScoped<ICategoryDomainService, CategoryDomainService>();
            services.AddScoped<IPaymentMethodDomainService, PaymentMethodDomainService>();
            services.AddScoped<ITransactionDomainService, TransactionDomainService>();
            services.AddScoped<IUserDomainService, UserDomainService>();

            #endregion

            #region Application

            services.AddServicesMediatR();

            #endregion

            return services;

        }

        /// <summary>
        /// Add mediator services 
        /// </summary>
        /// <param name="services">Service collection object</param>
        private static IServiceCollection AddServicesMediatR(this IServiceCollection services)
        {

            List<string> assembliesNames = new()
            {
                "RSoft.Entry.GrpcService",
                "RSoft.Entry.Application"
            };


            assembliesNames
                .ForEach(assemblyName =>
                {
                    var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                    services.AddMediatR(assembly);
                });

            return services;

        }

    }
}
