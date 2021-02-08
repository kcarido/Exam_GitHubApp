using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace CARIDO_GitHubApp.WebHandlers
{
    public abstract class WebHandler
    {
        protected static CookieContainer cookies;
        protected static HttpClientHandler httpHandler;

        public WebHandler()
        {
            cookies = new CookieContainer();
            httpHandler = new HttpClientHandler
            {
                CookieContainer = cookies
            };
        }

        protected HttpClientHandler GetHttpHandler()
        {
            return httpHandler;
        }

        protected void ShowError(string statusCode, string uri)
        {
            Debug.WriteLine("HTTP" + statusCode + " while accessing " + uri);
        }

        protected void ShowCookies(Uri uri)
        {
            IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
            foreach (Cookie cookie in responseCookies)
                Debug.WriteLine(cookie.Name + ": " + cookie.Value);
        }
    }
}
