namespace optial_backend.Helpers
{
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersCollectionName { get; set; }
    }

    public interface IAppSettings
    {
        string Secret { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string UsersCollectionName { get; set; }
    }
}