using Autofac;
using EchoBotV3.BotOverrides.Autofac;
using EchoBotV3.State;
using Microsoft.Bot.Builder.Dialogs;
using System.Reflection;
using System.Web.Http;

namespace EchoBotV3
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Conversation.UpdateContainer(builder =>
            {
                builder.RegisterModule(new DefaultExceptionMessageOverrideModule());
                var resolveAssembly = Assembly.GetCallingAssembly();
                builder.RegisterModule(new SqlBotDataStoreModule(resolveAssembly));

            });

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
