using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace CARIDO_GitHubApp.ScrapHandlers
{
    abstract class ScrapBaseHandler : IScrapHandler
    {
        protected HtmlDocument page { get; }

        protected ScrapBaseHandler(string html)
        {
            page = new HtmlDocument();
            page.LoadHtml(html);
        }

        public abstract object getDetails();

        protected string getAttributeValue(string path, string attr = "value")
        {
            if (page != null)
            {
                return page.DocumentNode.SelectSingleNode(path).Attributes[attr].Value;
            }
            return null;
        }
        protected string[] getAttributeListValue(string path, string attr = null, bool isInput = false)
        {
            IEnumerable<string> listValues = new List<string>();
            if (page != null)
            {
                if (isInput)
                {
                    listValues = page.DocumentNode.SelectNodes(path).Select(element =>
                    {
                        return element.Attributes[attr].Value;
                    });
                }
                else
                {
                    listValues = page.DocumentNode.SelectNodes(path).Select(element =>
                    {
                        return element.InnerHtml;
                    });
                }
            }
            return listValues.ToArray();
        }
    }
}
