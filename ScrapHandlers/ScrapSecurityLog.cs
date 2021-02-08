using CARIDO_GitHubApp.Models;

namespace CARIDO_GitHubApp.ScrapHandlers
{
    class ScrapSecurityLog : ScrapBaseHandler
    {
        public ScrapSecurityLog(string html) : base(html) { }

        public override object getDetails()
        {
            System.Collections.Generic.List<SecurityLogModel> list = new System.Collections.Generic.List<SecurityLogModel>();

            var eventNames = getAttributeListValue("//div[@class='TableObject-item--primary']/div[@class='actor-and-action']/span[@class='audit-type']/a");
            var eventTimes = getAttributeListValue("//div[@class='TableObject-item--primary']/div/relative-time");
            if (eventNames.Length == 1)
            {
                for (var ctr = 0; ctr < eventNames.Length; ctr++)
                {
                    list.Add(new SecurityLogModel(eventNames[ctr], eventTimes[ctr]));
                }
            }
            return list;
        }
    }
}
