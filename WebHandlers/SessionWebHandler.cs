using CARIDO_GitHubApp.Models;
using CARIDO_GitHubApp.ScrapHandlers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace CARIDO_GitHubApp.WebHandlers
{
    public class SessionWebHandler : WebHandler, ISessionHandler
    {
        /***********************************************************************
         * Simulating Credentials Form Submit from "https://github.com/login"
         * -> Proper credential validation not working possibly due to:
         * --> browser stats are not being sent
         * --> required parameter not being passed
         * -> Username and password API Athentication has been disabled
         ***********************************************************************/
        public string CreateSession(string username, string password)
        {
            LoginModel formContent = null;
            using var client = new HttpClient(GetHttpHandler());
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "UTF-8");
            using (HttpResponseMessage response = client.GetAsync("https://github.com/login").Result)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("referrer", "https://github.com/login");
                    cookies.SetCookies(new System.Uri("https://github.com/login"), "tz=Asia/Shanghai");
                    IEnumerable<string> cookie = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
                    //showCookies(new System.Uri("https://github.com/login"));
                    using HttpContent content = response.Content;
                    IScrapHandler scrapHandler = new ScrapLogin(content.ReadAsStringAsync().Result);
                    formContent = (LoginModel)scrapHandler.getDetails();
                    formContent.password = password;
                    formContent.login = username;
                }
                else
                {
                    ShowError(response.StatusCode.ToString(), response.RequestMessage.RequestUri.ToString());
                }
            }

            var stringContent = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("authenticity_token", formContent.authenticity_token),
                    new KeyValuePair<string, string>("timestamp_secret", formContent.timestamp_secret),
                    new KeyValuePair<string, string>("timestamp", formContent.timestamp),
                    new KeyValuePair<string, string>("password ", formContent.password),
                    new KeyValuePair<string, string>("login", formContent.login),
                    new KeyValuePair<string, string>("commit ", "Sign in"),
                    new KeyValuePair<string, string>("webauthn-support ", "supported"),
                    new KeyValuePair<string, string>("webauthn-iuvpaa-support", "unsupported")
                });
            using (HttpResponseMessage response = client.PostAsync("https://github.com/session", stringContent).Result)
            {
                IEnumerable<string> cookie = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //showCookies(new System.Uri("https://github.com/session"));
                }
                else
                {
                    ShowError(response.StatusCode.ToString(), response.RequestMessage.RequestUri.ToString());
                }
            }
            using (HttpResponseMessage response = client.GetAsync("https://github.com/settings/security-log").Result)
            {
                using HttpContent content = response.Content;
                IScrapHandler scrapHandler = new ScrapSecurityLog(content.ReadAsStringAsync().Result);
                return Newtonsoft.Json.JsonConvert.SerializeObject(scrapHandler.getDetails());
            }
        }
    }
}
