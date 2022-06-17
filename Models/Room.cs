using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi_v2.Models
{
    public class Room
    {
        [BsonId]
        public string RoomId { get; set; }
        public string RoomType { get; set; }
        public string RoomNumber { get; set; }
        public int RoomRate { get; set; }
        public int WaterMeter { get; set; }
        public int PowerMeter { get; set; }
        public string Floor { get; set; }
        public string RoomStatus { get; set; }
        public string Status { get; set; }
        public string UserDetailId { get; set; }
    }
}