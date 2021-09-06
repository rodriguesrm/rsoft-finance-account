﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RSoft.Account.Infra.Extensions
{

    /// <summary>
    /// Database extensions
    /// </summary>
    public static class DatabaseExtension
    {

        /// <summary>
        /// Create/Update database by migration tool
        /// </summary>
        /// <param name="app">Application builder object instance</param>
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app, ILogger logger)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AccountContext>())
                {
                    logger.LogInformation($"Migrating database {nameof(AccountContext)}");
                    context.Database.Migrate();
                    logger.LogInformation($"Database migrated");
                }
            }

            return app;
        }

    }

}
