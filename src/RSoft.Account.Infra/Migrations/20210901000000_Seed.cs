using Microsoft.EntityFrameworkCore.Migrations;
using RSoft.Account.Infra.Tables;
using System;

namespace RSoft.Account.Infra.Migrations
{
    public abstract class InitialSeed : Migration
    {

        #region Local objects/variables

        private readonly bool _isProd;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new InitialSeed instance
        /// </summary>
        public InitialSeed() : base()
        {

            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _isProd = env.ToLower() == "production";

        }

        #endregion

        /// <summary>
        /// Seed initial data
        /// </summary>
        /// <param name="migrationBuilder">A MicrationBuilder object instance</param>
#pragma warning disable CA1822 // Mark members as static
        protected void Seed(MigrationBuilder migrationBuilder)
        {

            Guid userId = new("745991cc-c21f-4512-ba8f-9533435b64ab");

            migrationBuilder.Sql("set foreign_key_checks=0");

            migrationBuilder.InsertData
            (
                nameof(User),
                new string[]
                {
                    nameof(User.Id),
                    nameof(User.FirstName),
                    nameof(User.LastName),
                    nameof(User.IsActive)
                },
                new object[] { userId, "Admin", "RSoft", true }
            );

            migrationBuilder.Sql("set foreign_key_checks=1");

        }
#pragma warning restore CA1822 // Mark members as static

    }
}
