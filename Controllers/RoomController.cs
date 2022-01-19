using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DMSapi_v2.Models;
using DMSapi_v2.Services;

namespace DMSapi_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;
        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public ActionResult<List<Room>> GetAllRoom() => _roomService.GetAllRoom();

        [HttpGet("{id}")]
        public ActionResult<Room> GetRoomById(string id)
        {
            var room = _roomService.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }
        [HttpGet("{roomNumber}")]
        public ActionResult<Room> GetRoomByNumber(string roomNumber)
        {
            var room = _roomService.GetRoomByRoomNumber(roomNumber);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }
        [HttpGet("{roomType}")]
        public  ActionResult<List<Room>>  GetRoomByType(string roomType)
        {
            var room = _roomService.GetRoomByType(roomType);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }
        [HttpGet("{roomStatus}")]
        public  ActionResult<List<Room>>  GetRoomByStatus(string roomStatus)
        {
            var room = _roomService.GetRoomByStatus(roomStatus);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }
        [HttpGet("{floor}")]
        public ActionResult<List<Room>> GetRoomByFloor(string floor)
        {
            var room = _roomService.GetRoomByFloor(floor);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }
        [HttpPost]
        public Room CreateRoom([FromBody] Room room)
        {
            var data = _roomService.GetAllRoomForApi();
            var count = data.Count();
            var id = "R00" + count.ToString();
            room.RoomId = "R00" + count.ToString();
            room.RoomId = id;
            room.Status = "Open";
            _roomService.CreateRoom(room);
            return room;
        }

        [HttpPut("{id}")]
        public IActionResult EditRoom([FromBody] Room room, string id)
        {
            var rooms = _roomService.GetRoomById(id);
            if (rooms == null)
            {
                return NotFound();
            }
            room.RoomId = id;
            _roomService.EditRoom(id, room);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteRoom(string id)
        {
            var rooms = _roomService.GetRoomById(id);
            var statusChange = rooms.Status;
            if (rooms == null)
            {
                return NotFound();
            }
            if (statusChange == "Open")
            {
                statusChange = "Close";
            }
            rooms.Status = statusChange;
            _roomService.DeleteRoom(id, rooms);
            return NoContent();
        }
    }
}