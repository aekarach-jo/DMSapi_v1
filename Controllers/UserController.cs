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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUser() => _userService.GetAllUser();

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(string id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUserByUserName(string Id)
        {
            var user = _userService.GetUserByUserName(Id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public User CreateUser([FromBody] User user)
        {
            var data = _userService.GetAllUserForApi();
            int number = data.Count();
            user.UserId = "USER00" + number.ToString();
            _userService.CreateUser(user);
            return user;
        }

        [HttpPut("{id}")]
        public IActionResult EditUser([FromBody] User user, string id)
        {
            var users = _userService.GetUserById(id);
            if (users == null)
            {
                return NotFound();
            }
            user.UserId = id;
            _userService.EditUser(id, user);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteUser(string id)
        {
            var users = _userService.GetUserById(id);
            var statusChange = users.Status;
            if (users == null)
            {
                return NotFound();
            }
            if (statusChange == "Open")
            {
                statusChange = "Close";
            }
            users.Status = statusChange;
            _userService.DeleteUser(id, users);
            return NoContent();
        }

        [HttpGet("{userNmae}")]
        public string CheckUser (string userNmae)
        {
            var ids = _userService.CheckUserName(userNmae);
            if(ids == null)
            {
                return "ไอดีนี้สามารถใช้งานได้";
            }
            return "ไอดีนี้มีในระบบ";
        }

        [HttpGet("{username}/{password}")]
        public ActionResult<User> CheckUserAndPassword (string username, string password)
        {
            var userNameAndPassword = _userService.CheckUserNameAndPassword(username,password);
            if(userNameAndPassword == null)
            {
                return NotFound();
            }
            return userNameAndPassword;
        }
    }
}