using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi_v2.Models
{
    public class Payment
    {
        [BsonId]
        public string PaymentId { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentStatus { get; set; }
        public string PayFine { get; set; }
        public string PayImage { get; set; }
        public string PayNote { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Status { get; set; }
        public string InvoiceId { get; set; }
        public string UserId { get; set; }
    }
}