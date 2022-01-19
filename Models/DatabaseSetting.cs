namespace DMSapi_v2.Models
{
    public class DatabaseSetting : IDatabaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UserCollection { get; set; }
        public string RoomCollection { get; set; }
    }
    public interface IDatabaseSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string UserCollection { get; set; }
        string RoomCollection { get; set; }
    }
}