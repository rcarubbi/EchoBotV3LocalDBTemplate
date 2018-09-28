using System.Data.Entity.Infrastructure;

namespace EchoBotV3.State
{
    public class SqlBotDataContextFactory : IDbContextFactory<SqlBotDataContext>
    {
        public SqlBotDataContext Create()
        {
            return new SqlBotDataContext("BotDataContextConnectionString");
        }
    }
}