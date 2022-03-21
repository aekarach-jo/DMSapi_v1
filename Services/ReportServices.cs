using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;

using DMSapi_v2.Models;
namespace DMSapi_v2.Services
{
    public class ReportService
    {
        private readonly IMongoCollection<Report> _report;

        public ReportService(DatabaseSetting setting)
        {
            var Client = new MongoClient(setting.ConnectionString);
            var database = Client.GetDatabase(setting.DatabaseName);
            var filter = Builders<Report>.Filter.Where(report => report.Status == "Open");
            _report = database.GetCollection<Report>(setting.ReportCollection);
        }

        public List<Report> GetAllReport() => _report.Find(report => report.Status == "Open").ToList();
        public List<Report> GetAllReportForApi() => _report.Find(report => true).ToList();
        public Report GetReportById(string reportId) => _report.Find(report => report.ReportId == reportId).FirstOrDefault();
        public Report GetReportByReportNumber(string reportNumber) => _report.Find(report => report.ReportNumber == reportNumber).FirstOrDefault();
        public List<Report> GetReportByStatus(string reportStatus) => _report.Find(report => report.ReportStatus == reportStatus).ToList();
        public Report CreateReport(Report report)
        {
            _report.InsertOne(report);
            return report;
        }

        public void EditReport(string reportId, Report reportBody) => _report.ReplaceOne(report => report.ReportId == reportId, reportBody);
        public void DeleteReport(string reportId, Report reportBody) => _report.ReplaceOne(report => report.ReportId == reportId, reportBody);

    }
}

