using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Infra
{

    /// <summary>
    /// Entry context factory
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Config class used only for generate migration")]
    public class EntryContextFactory : IDesignTimeDbContextFactory<EntryContext>
    {

        ///<inheritdoc/>
        public EntryContext CreateDbContext(string[] args)
        {
            string connectionString = "Server=192.168.3.1;Port=3306;Database=rsoft_entry;Uid=root;password=RR.MySqlDev;";
            DbContextOptions options = new DbContextOptionsBuilder().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
            return new EntryContext(options);
        }
    }
}
