using System.Net.Http;

namespace CARIDO_GitHubApp.WebHandlers
{
    interface IWebHandler
    {
        HttpClientHandler getHttpHandler();
    }
}
