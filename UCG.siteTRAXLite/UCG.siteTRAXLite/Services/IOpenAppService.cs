namespace UCG.siteTRAXLite.Services
{
    public interface IOpenAppService
    {
        Task<bool> LaunchApp(string packageName);
    }
}
