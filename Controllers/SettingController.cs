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
    public class SettingController : ControllerBase
    {
        private readonly SettingService _settingService;
        public SettingController(SettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public ActionResult<List<Setting>> GetAllSetting() => _settingService.GetAllSetting();

        [HttpGet("{id}")]
        public ActionResult<Setting> GetSettingById(string id)
        {
            var setting = _settingService.GetSettingById(id);
            if (setting == null)
            {
                return NotFound();
            }
            return setting;
        }

        [HttpPost]
        public Setting CreateSetting([FromBody] Setting setting)
        {
            var data = _settingService.GetAllSettingForApi();
            var count = data.Count();
            var id = "S00" + count.ToString();
            setting.SettingId = id;
            setting.Status = "Open";
            _settingService.CreateSetting(setting);
            return setting;
        }

        [HttpPut("{id}")]
        public IActionResult EditSetting([FromBody] Setting setting, string id)
        {
            var settings = _settingService.GetSettingById(id);
            if (settings == null)
            {
                return NotFound();
            }
            setting.SettingId = id;
            _settingService.EditSetting(id, setting);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteSetting(string id)
        {
            var settings = _settingService.GetSettingById(id);
            var statusChange = settings.Status;
            if (settings == null)
            {
                return NotFound();
            }
            if (statusChange == "Open")
            {
                statusChange = "Close";
            }
            settings.Status = statusChange;
            _settingService.DeleteSetting(id, settings);
            return NoContent();
        }
    }
}