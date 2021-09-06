using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RSoft.Account.Infra
{

    /// <summary>
    /// Account context factory
    /// </summary>
    public class AccountContextFactory : IDesignTimeDbContextFactory<AccountContext>
    {

        ///<inheritdoc/>
        public AccountContext CreateDbContext(string[] args)
        {
            string connectionString = "Server=192.168.3.1;Port=3306;Database=rsoft_account;Uid=root;password=RR.MySqlDev;";
            DbContextOptions options = new DbContextOptionsBuilder().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
            return new AccountContext(options);
        }
    }
}
