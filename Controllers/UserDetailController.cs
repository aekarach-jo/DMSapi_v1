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
    public class UserDetailController : ControllerBase
    {
        private readonly UserDetailService _userDetailService;
        public UserDetailController(UserDetailService userDetailService)
        {
            _userDetailService = userDetailService;
        }

        [HttpGet]
        public ActionResult<List<UserDetail>> GetAllUserDetail() => _userDetailService.GetAllUserDetail();

        [HttpGet("{id}")]
        public ActionResult<UserDetail> GetUserDetailById(string id)
        {
            var userDetail = _userDetailService.GetUserDetailById(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            return userDetail;
        }
        [HttpGet("{firstName}")]
        public ActionResult<UserDetail> GetUserDetailByFirstName(string firstName)
        {
            var userDetail = _userDetailService.GetUserDetailByFirstName(firstName);
            if (userDetail == null)
            {
                return NotFound();
            }
            return userDetail;
        }
        [HttpGet("{tel}")]
        public ActionResult<UserDetail> GetUserDetailByTelephone(string tel)
        {
            var userDetail = _userDetailService.GetUserDetailByTelephone(tel);
            if (userDetail == null)
            {
                return NotFound();
            }
            return userDetail;
        }

        [HttpPost]
        public UserDetail CreateUserDetail([FromBody] UserDetail userDetail)
        {
            var data = _userDetailService.GetAllUserDetailForApi();
            int number = data.Count();
            userDetail.UserDetailId = "UD00" + number.ToString();
            _userDetailService.CreateUserDetail(userDetail);
            return userDetail;
        }

        [HttpPut("{id}")]
        public IActionResult EditUserDetail([FromBody] UserDetail userDetail, string id)
        {
            var userDetails = _userDetailService.GetUserDetailById(id);
            if (userDetails == null)
            {
                return NotFound();
            }
            userDetail.UserDetailId = id;
            _userDetailService.EditUserDetail(id, userDetail);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteUserDetail(string id)
        {
            var userDetails = _userDetailService.GetUserDetailById(id);
            var statusChange = userDetails.Status;
            if (userDetails == null)
            {
                return NotFound();
            }
            if (statusChange == "Open")
            {
                statusChange = "Close";
            }
            userDetails.Status = statusChange;
            _userDetailService.DeleteUserDetail(id, userDetails);
            return NoContent();
        }
    }
}