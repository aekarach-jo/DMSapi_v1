using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;

using DMSapi_v2.Models;
namespace DMSapi_v2.Services
{
    public class SettingService
    {
        private readonly IMongoCollection<Setting> _setting;

        public SettingService(DatabaseSetting setting)
        {
            var Client = new MongoClient(setting.ConnectionString);
            var database = Client.GetDatabase(setting.DatabaseName);
            var filter = Builders<Setting>.Filter.Where(setting => setting.Status == "Open");
            _setting = database.GetCollection<Setting>(setting.SettingCollection);
        }

        public List<Setting> GetAllSetting() => _setting.Find(setting => setting.Status == "Open").ToList();
        public List<Setting> GetAllSettingForApi() => _setting.Find(setting => true).ToList();
        public Setting GetSettingById(string settingId) => _setting.Find(setting => setting.SettingId == settingId).FirstOrDefault();
        public Setting CreateSetting(Setting setting)
        {
            _setting.InsertOne(setting);
            return setting;
        }

        public void EditSetting(string settingId, Setting settingBody) => _setting.ReplaceOne(setting => setting.SettingId == settingId, settingBody);
        public void DeleteSetting(string settingId, Setting settingBody) => _setting.ReplaceOne(setting => setting.SettingId == settingId, settingBody);

    }
}

