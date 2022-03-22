using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;

using DMSapi_v2.Models;
namespace DMSapi_v2.Services
{
    public class RoomService
    {
        private readonly IMongoCollection<Room> _room;

        public RoomService(DatabaseSetting setting)
        {
            var Client = new MongoClient(setting.ConnectionString);
            var database = Client.GetDatabase(setting.DatabaseName);
            var filter = Builders<Room>.Filter.Where(room => room.Status == "Open");
            _room = database.GetCollection<Room>(setting.RoomCollection);
        }

        public List<Room> GetAllRoom() => _room.Find(room => room.Status == "Open").ToList();
        public List<Room> GetAllRoomForApi() => _room.Find(room => true).ToList();
        public Room GetRoomById(string roomId) => _room.Find(room => room.RoomId == roomId).FirstOrDefault();
        public Room GetRoomByRoomNumber(string roomNumber) => _room.Find(room => room.RoomNumber == roomNumber).FirstOrDefault();
        public List<Room> GetRoomByType(string roomType) => _room.Find(room => room.RoomType == roomType).ToList();
        public List<Room> GetRoomByStatus(string roomStatus) => _room.Find(room => room.RoomStatus == roomStatus).ToList();
        public List<Room> GetRoomByFloor(string floor) => _room.Find(room => room.Floor == floor).ToList();
        public Room CreateRoom(Room room)
        {
            _room.InsertOne(room);
            return room;
        }

        public void EditRoom(string roomId, Room roomBody) => _room.ReplaceOne(room => room.RoomId == roomId, roomBody);
        public void DeleteRoom(string roomId, Room roomBody) => _room.ReplaceOne(room => room.RoomId == roomId, roomBody);

    }
}

