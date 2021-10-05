using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSoft.Entry.GrpcClient;
using RSoft.Entry.GrpcClient.Abstractions;
using RSoft.Entry.GrpcClient.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RSoft.EntrySdk.Test
{
    class Program
    {

        #region Container/Configuration

        private static IConfiguration _configuration;
        public static IConfiguration Configuration 
        { 
            get
            {

                if (_configuration == null)
                {   
                    IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile($"appsettings.json", true, true)
                        .AddEnvironmentVariables();
                    _configuration = builder.Build();
                }
                return _configuration;

            }
        }


        private static IServiceCollection _serviceCollection;
        public static IServiceCollection ServiceCollection
        { 
            get
            {
                if (_serviceCollection == null)
                {
                    _serviceCollection = new ServiceCollection();
                    _serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
                    _serviceCollection.AddEntryGrpcServiceClient(Configuration);
                }
                return _serviceCollection;
            }
        }

        private static IServiceProvider _serviceProvider;
        public static IServiceProvider ServiceProvider 
        { 
            get
            {
                _serviceProvider ??= ServiceCollection.BuildServiceProvider();
                return _serviceProvider;
            }
        }

        #endregion

        #region Main methods

        static async Task Main(string[] args)
        {

            //System.Threading.Thread.Sleep(10000);

            GrpcCategoryServiceProvider categoryClient = ServiceProvider.GetService<GrpcCategoryServiceProvider>();
            //TODO: Make call authentication api
            categoryClient.SetToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiNzQ1OTkxY2MtYzIxZi00NTEyLWJhOGYtOTUzMzQzNWI2NGFiIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6IlJTb2Z0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoibWFzdGVyQHNlcnZlci5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoiVXNlciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZ3JvdXBzaWQiOlsiRW50cnkgU2VydmljZSIsIkF1dGhlbnRpY2F0aW9uIFNlcnZpY2UiXSwibmJmIjoxNjMzNDU1NzUwLCJleHAiOjE2MzM0NzAyNzAsImlzcyI6IlJTb2Z0LkF1dGguRGV2IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTAwIn0.fRPZ_Y2ToN7-Il_Zk1b2YYz064n2rp3QUqlLl8KEpuE");

            CreateCategoryResponse resultCreate = await categoryClient.CreateCategory("MY_CATEGORY");
            Console.WriteLine($"Id generated: {resultCreate.ResponseData.Value}");

            CategoryDetailResponse resultGet = await categoryClient.GetCategory(resultCreate.ResponseData.Value);
            Console.WriteLine($"Category found: {resultGet.ResponseData.Name}");

            await categoryClient.UpdateCategory(resultCreate.ResponseData.Value, $"NEW_NAME_CATEGORY{DateTime.UtcNow.Ticks}");
            Console.WriteLine("Category Updated");

            await categoryClient.DisableCategory(resultCreate.ResponseData.Value);
            Console.WriteLine("Category Disabled");

            await categoryClient.EnableCategory(resultCreate.ResponseData.Value);
            Console.WriteLine("Category Enabled");

            ListCategoryDetailResponse categories = await categoryClient.ListCategory();
            foreach (var category in categories.ResponseData)
            {
                Console.WriteLine($"Id: {category.Id} - Name: {category.Name}");
            }

            Console.ReadKey();
        }

        #endregion

    }
}
