using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi_v2.Models
{
    public class Report
    {
        [BsonId]
        public string ReportId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public string ReportNumber { get; set; }
        public string ReportStatus { get; set; }
        public string RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string Status { get; set; }
    }
}