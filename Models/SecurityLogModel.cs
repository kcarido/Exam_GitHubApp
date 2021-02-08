namespace CARIDO_GitHubApp.Models
{
    class SecurityLogModel
    {
        string eventName { get; set; }
        string eventTime { get; set; }

        public SecurityLogModel(string name, string time)
        {
            this.eventName = name;
            this.eventTime = time;
        }
    }
}
