




using System.Web.Http;

namespace Michaellrg.MOIP.Web
{
    public static class WebHookConfig
    {
        public static void Register(HttpConfiguration config)
        {

			config.InitializeReceiveSlackWebHooks();

        }
    }
}
