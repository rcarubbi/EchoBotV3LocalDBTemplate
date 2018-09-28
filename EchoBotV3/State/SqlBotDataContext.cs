﻿using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace EchoBotV3.State
{
    public class SqlBotDataContext : DbContext
    {
        
        public SqlBotDataContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<SqlBotDataContext>(null);
        }

        /// <summary>
        /// Throw if the database or SqlBotDataEntities table have not been created.
        /// </summary>
        internal static void AssertDatabaseReady()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BotDataContextConnectionString"]
                .ConnectionString;
            using (var context = new SqlBotDataContext(connectionString))
            {
                if (!context.Database.Exists())
                    throw new ArgumentException("The sql database defined in the connection has not been created. See https://github.com/Microsoft/BotBuilder-Azure/tree/master/CSharp");

                if (context.Database.SqlQuery<int>(@"IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SqlBotDataEntities') 
                                                                    SELECT 1
                                                                ELSE
                                                                    SELECT 0").SingleOrDefault() != 1)
                    throw new ArgumentException("The SqlBotDataEntities table has not been created in the database. See https://github.com/Microsoft/BotBuilder-Azure/tree/master/CSharp");
            }
        }

        public DbSet<SqlBotDataEntity> BotData { get; set; }
    }
}