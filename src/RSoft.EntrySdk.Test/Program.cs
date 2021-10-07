﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSoft.Entry.GrpcClient;
using RSoft.Entry.GrpcClient.Abstractions;
using RSoft.Entry.GrpcClient.Models;
using RSoft.Entry.GrpcClient.Providers;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RSoft.EntrySdk.Test
{
    class Program
    {

        #region Local object/variable

        private static Guid? _categoryId;
        private static Guid? _entryId;

        #endregion

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
            System.Threading.Thread.Sleep(10000);

            TokenResponse token = await Authenticate();
            //await CategoryTest(token);
            //await EntryTest(token);
            await AccrualPeriodTest(token);

            Console.ReadKey();
        }

        private static async Task CategoryTest(TokenResponse token)
        {

            Console.WriteLine("--- CATEGORY ---------------------------");

            IGrpcCategoryServiceProvider provider = ServiceProvider.GetService<IGrpcCategoryServiceProvider>();
            provider.SetToken(token.Token);

            CreateCategoryResponse resultCreate = await provider.CreateCategory("MY_CATEGORY");
            Console.WriteLine($"Id generated: {resultCreate.ResponseData.Value}");
            _categoryId = resultCreate.ResponseData.Value;

            CategoryDetailResponse resultGet = await provider.GetCategory(resultCreate.ResponseData.Value);
            Console.WriteLine($"Category found: {resultGet.ResponseData.Name}");

            await provider.UpdateCategory(resultCreate.ResponseData.Value, $"NEW_NAME_CATEGORY{DateTime.UtcNow.Ticks}");
            Console.WriteLine("Category Updated");

            await provider.DisableCategory(resultCreate.ResponseData.Value);
            Console.WriteLine("Category Disabled");

            await provider.EnableCategory(resultCreate.ResponseData.Value);
            Console.WriteLine("Category Enabled");

            ListCategoryDetailResponse categories = await provider.ListCategory();
            foreach (var category in categories.ResponseData)
            {
                Console.WriteLine($"Id: {category.Id} - Name: {category.Name}");
            }

        }

        private static async Task EntryTest(TokenResponse token)
        {

            Console.WriteLine("--- ENTRY ------------------------------");

            IGrpcEntryServiceProvider provider = ServiceProvider.GetService<IGrpcEntryServiceProvider>();
            provider.SetToken(token.Token);

            CreateEntryResponse resultCreate = await provider.CreateEntry("ENTRY_NAME", _categoryId);
            Console.WriteLine($"Id generated: {resultCreate.ResponseData.Value}");
            _entryId = resultCreate.ResponseData.Value;

            EntryDetailResponse resultGet = await provider.GetEntry(resultCreate.ResponseData.Value);
            Console.WriteLine($"Entry found: {resultGet.ResponseData.Name}");

            await provider.UpdateEntry(resultCreate.ResponseData.Value, $"NEW_NAME_ENTRY{DateTime.UtcNow.Ticks}", _categoryId);
            Console.WriteLine("Entry Updated");

            await provider.DisableEntry(resultCreate.ResponseData.Value);
            Console.WriteLine("Entry Disabled");

            await provider.EnableEntry(resultCreate.ResponseData.Value);
            Console.WriteLine("Entry Enabled");

            ListEntryDetailResponse entities = await provider.ListEntry();
            foreach (var Entry in entities.ResponseData)
            {
                Console.WriteLine($"Id: {Entry.Id} - Name: {Entry.Name}");
            }

        }

        private static async Task AccrualPeriodTest(TokenResponse token)
        {

            Console.WriteLine("--- ACCRUAL PERIOD ---------------------");

            IGrpcAccrualPeriodServiceProvider provider = ServiceProvider.GetService<IGrpcAccrualPeriodServiceProvider>();
            provider.SetToken(token.Token);

            DateTime date = DateTime.UtcNow.AddYears(7);

            StartPeriodResponse startResult = await provider.StartPeriod(date.Year, date.Month);
            Console.WriteLine($"Started period for {date.Year}.{date.Month} was {(startResult.StatusCode == Grpc.Core.StatusCode.OK ? "Success" : "Fail")}");

            ClosePeriodResponse closeResult = await provider.ClosePeriod(date.Year, date.Month);
            Console.WriteLine($"Close period for {date.Year}.{date.Month} was {(closeResult.StatusCode == Grpc.Core.StatusCode.OK ? "Success" : "Fail")}");

            ListAccrualPeriodDetailResponse entities = await provider.ListPeriod();
            foreach (var entity in entities.ResponseData)
            {
                Console.WriteLine($"Year: {entity.Year} - Month: {entity.Month} => {(entity.IsClosed ? "CLOSED" : "OPEN")}");
            }

        }

        private static async Task<TokenResponse> Authenticate()
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://192.168.3.1:5100");
            httpClient.DefaultRequestHeaders.Add("app-key", "3f3b94db-d868-4cb3-8098-214a53eccc35");
            httpClient.DefaultRequestHeaders.Add("app-access", "cda09ab8-2b05-49e8-8eec-60ad6cfea2e5");

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            StringContent body = new
            (
                JsonSerializer.Serialize(new { Login = "admin", Password = "master@soft" }, options),
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage resp = await httpClient.PostAsync("/api/v1.0/Auth", body, default);

            string result = await resp.Content.ReadAsStringAsync();
            TokenResponse token = JsonSerializer.Deserialize<TokenResponse>(result, options);
            return token;
        }

        class TokenResponse
        {
            public string Token { get; set; }
            public DateTime? ExpirationDate { get; set; }
        }

        #endregion

    }
}
