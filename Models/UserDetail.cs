using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi_v2.Models
{
    public class UserDetail
    {
        [BsonId]
        public string UserDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Birthday { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdCard { get; set; } //เลขบัตร ปปช
        public string UserStatus { get; set; } //สถานะ เข้าอยู่/ย้ายออกแล้ว
        public int Deposit { get; set; } //เงินมัดจำ
        public DateTime? BookingDate { get; set; } //วันที่จอง
        public DateTime? BookingDateOfStay { get; set; } //วันที่พร้อมเข้าอยู่
        public DateTime? DateIn { get; set; } //เก็บข้อมูลวันที่สร้างข้อมูล
        public DateTime? DateOut { get; set; } //เก็บข้อมูลวันที่ลบข้อมูล
        public string RoomNumber { get; set; }
        public string Status { get; set; }


    }
}