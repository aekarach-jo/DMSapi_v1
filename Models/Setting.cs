using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi_v2.Models
{
    public class Setting
    {
        [BsonId]
        public string SettingId { get; set; }
        public int WaterPrice { get; set; }
        public int PowerPrice { get; set; }
        public int CenterService { get; set; }
        public int RoomrateTypeFan { get; set; }
        public int RoomrateTypeAir { get; set; }
        public DateTime CreationDatetime { get; set; }
        public string Status { get; set; }

    }
}