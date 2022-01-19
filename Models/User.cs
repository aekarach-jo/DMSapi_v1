using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi_v2.Models
{
    public class User
    {
        [BsonId]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Permission { get; set; }
        public string UserStatus { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? DeletionDateTime { get; set; }
        public string UserDetailId { get; set; }
        public string Status { get; set; }

    }
}