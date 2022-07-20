using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DMSapi_v2.Models;
using DMSapi_v2.Services;
using System.IO;
using System.Net.Http.Headers;

namespace DMSapi_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;
        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }


        // [HttpPost, DisableRequestSizeLimit]
        // public IActionResult Upload()
        // {
        //     try
        //     {
        //         var file = Request.Form.Files[0];
        //         var folderName = Path.Combine("Resources", "Images");
        //         var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //         if (file.Length > 0)
        //         {
        //             var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //             var fullPath = Path.Combine(pathToSave, fileName);
        //             var dbPath = Path.Combine(folderName, fileName);

        //             using (var stream = new FileStream(fullPath, FileMode.Create))
        //             {
        //                 file.CopyTo(stream);
        //             }

        //             return Ok(new { dbPath });
        //         }
        //         else
        //         {
        //             return BadRequest();
        //         }
        //     }
        //     catch (System.Exception)
        //     {
        //         return StatusCode(500, $"Internal Server Error");
        //     }
        // }

        [HttpGet]
        public ActionResult<List<Report>> GetAllReport() => _reportService.GetAllReport();

        [HttpGet("{id}")]
        public ActionResult<Report> GetReportById(string id)
        {
            var report = _reportService.GetReportById(id);
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }
        [HttpGet("{reportNumber}")]
        public ActionResult<Report> GetReportByNumber(string reportNumber)
        {
            var report = _reportService.GetReportByReportNumber(reportNumber);
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }
        [HttpGet("{reportRoomId}")]
        public ActionResult<Report> GetReportByRoomId(string reportRoomId)
        {
            var report = _reportService.GetReportByRoomId(reportRoomId);
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }

        [HttpGet("{reportStatus}")]
        public ActionResult<List<Report>> GetReportByStatus(string reportStatus)
        {
            var report = _reportService.GetReportByStatus(reportStatus);
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }

        [HttpPost]
        public ActionResult<Report> CreateReport(Report report)
        {
            var data = _reportService.GetAllReportForApi();
            var count = data.Count();
            var id = "Re00" + count.ToString();
            var num = "Re-" + count.ToString();
            report.ReportNumber = num;
            report.ReportId = id;
            report.Status = "Open";
            _reportService.CreateReport(report);
            return report;
        }

        [HttpPut("{id}")]
        public IActionResult EditReport([FromBody] Report report, string id)
        {
            var reports = _reportService.GetReportById(id);
            if (reports == null)
            {
                return NotFound();
            }
            report.ReportId = id;
            _reportService.EditReport(id, report);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteReport(string id)
        {
            var reports = _reportService.GetReportById(id);
            var statusChange = reports.Status;
            if (reports == null)
            {
                return NotFound();
            }
            if (statusChange == "Open")
            {
                statusChange = "Close";
            }
            reports.Status = statusChange;
            _reportService.DeleteReport(id, reports);
            return NoContent();
        }
    }
}