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
        public string RoomRate { get; set; }
        public string WaterMeter { get; set; }
        public string PowerMeter { get; set; }
        public string Floor { get; set; }
        public string RoomStatus { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
    }
}