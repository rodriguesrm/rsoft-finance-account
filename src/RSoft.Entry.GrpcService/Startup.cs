using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Cross.IoC;
using RSoft.Logs.Interceptors;
using RSoft.Entry.GrpcService.Services;
using RSoft.Entry.Infra.Extensions;
using RSoft.Lib.Common.Web.Extensions;
using RSoft.Lib.Web.Extensions;
using RSoft.Logs.Extensions;

namespace RSoft.Entry.GrpcService
{
    public class Startup
    {

        /// <summary>
        /// Creates a new instance of the application
        /// </summary>
        /// <param name="configuration">Configuration object</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Injected settings property
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(opt =>
            {
                opt.Interceptors.Add<RequestResponseInterceptor<Startup>>();
            });
            services.AddHttpContextAccessor();
            services.AddJwtToken(Configuration);
            services.AddEntryRegister(Configuration);
            services.AddMiddlewareLoggingOption(Configuration);
            services.AddCultureLanguage(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory factory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureLangague();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<EntryGrpcService>();
                endpoints.MapGrpcService<AccrualPeriodGrpcService>();
                endpoints.MapGrpcService<CategoryGrpcService>();
                endpoints.MapGrpcService<PaymentMethodGrpcService>();
                endpoints.MapGrpcService<TransactionGrpcService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

            ILogger logger = factory.CreateLogger("Microsoft.Hosting.Lifetime");
            app.MigrateDatabase(logger);

        }
    }
}
