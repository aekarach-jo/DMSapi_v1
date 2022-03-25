using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi_v2.Models
{
    public class Checkout
    {
        [BsonId]
        public string CheckoutId {get;set;}
        public string CheckoutStatus {get;set;}
        public DateTime CheckoutDate {get;set;}
        public string RoomNumber { get;set;}
        public string InvoiceStatus {get;set;} // ค้างชำระ , ไม่ค้างชำระ
        public string UserId {get;set;}
        public DateTime CreationDateTime {get;set;}
        public string Status {get;set;}

    }
}