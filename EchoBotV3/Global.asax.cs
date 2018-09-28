using Autofac;
using EchoBotV3.State;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System.Configuration;
using System.Web.Http;

namespace EchoBotV3
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Conversation.UpdateContainer(builder =>
            {
                var store = new SqlServerBotDataStore(ConfigurationManager.ConnectionStrings["BotDataContextConnectionString"]
                    .ConnectionString);
                builder.Register(c => store)
                    .As<IBotDataStore<BotData>>()
                    .AsSelf()
                    .SingleInstance();
            });

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
