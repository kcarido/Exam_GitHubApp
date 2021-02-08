namespace CARIDO_GitHubApp.Models
{
    public class LoginModel
    {
        public string login { get; set; }
        public string password { get; set; }
        public string authenticity_token { get; set; }
        public string timestamp_secret { get; set; }
        public string timestamp { get; set; }
    }
}
