namespace CARIDO_GitHubApp.WebHandlers
{
    interface ISessionHandler
    {
        string CreateSession(string username, string password);
    }
}
