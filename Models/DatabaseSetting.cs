namespace DMSapi_v2.Models
{
    public class DatabaseSetting : IDatabaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UserCollection { get; set; }
        public string RoomCollection { get; set; }
        public string UserDetailCollection { get; set; }
        public string InvoiceCollection { get; set; }
        public string ReportCollection { get; set; }
        public string PaymentCollection { get; set; }
        public string SettingCollection { get; set; }
        public string CheckoutCollection { get; set; }
    }
    public interface IDatabaseSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string UserCollection { get; set; }
        string RoomCollection { get; set; }
        string UserDetailCollection { get; set; }
        string InvoiceCollection { get; set; }
        string ReportCollection { get; set; }
        string PaymentCollection { get; set; }
        string SettingCollection { get; set; }
        string CheckoutCollection { get; set; }

    }
}