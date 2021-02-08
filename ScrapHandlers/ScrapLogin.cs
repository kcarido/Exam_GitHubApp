using CARIDO_GitHubApp.Models;

namespace CARIDO_GitHubApp.ScrapHandlers
{
    class ScrapLogin : ScrapBaseHandler
    {
        public ScrapLogin(string html) : base(html) { }

        public override object getDetails()
        {
            LoginModel model = new LoginModel();
            model.authenticity_token = getAttributeValue("//input[@name='authenticity_token']");
            model.timestamp_secret = getAttributeValue("//input[@name='timestamp_secret']");
            model.timestamp = getAttributeValue("//input[@name='timestamp']");
            return model;
        }
    }
}
