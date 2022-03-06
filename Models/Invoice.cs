using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi_v2.Models
{
    public class Invoice
    {
        [BsonId]
        public string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public int invoiceRoomRate { get; set; }
        public int WaterMeterOld { get; set; }
        public int WaterMeterNew { get; set; }
        public int WaterMeterUnit { get; set; }
        public int PowerMeterOld { get; set; }
        public int PowerMeterNew { get; set; }
        public int PowerMeterUnit { get; set; }
        public int CenterCervice { get; set; }
        public string InvoiceStatus { get; set; }
        public int InvoiceTotal { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Status { get; set; }
        public Room RoomId { get; set; }

    }
}