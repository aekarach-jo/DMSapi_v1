using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;

using DMSapi_v2.Models;
namespace DMSapi_v2.Services
{
    public class InvoiceService
    {
        private readonly IMongoCollection<Invoice> _invoice;

        public InvoiceService(DatabaseSetting setting)
        {
            var Client = new MongoClient(setting.ConnectionString);
            var database = Client.GetDatabase(setting.DatabaseName);
            var filter = Builders<Invoice>.Filter.Where(invoice => invoice.Status == "Open");
            _invoice = database.GetCollection<Invoice>(setting.InvoiceCollection);
        }

        public List<Invoice> GetAllInvoice() => _invoice.Find(invoice => invoice.Status == "Open").ToList();
        public List<Invoice> GetAllInvoiceForApi() => _invoice.Find(invoice => true).ToList();
        public Invoice GetInvoiceById(string invoiceId) => _invoice.Find(invoice => invoice.InvoiceId == invoiceId).FirstOrDefault();
        public Invoice GetInvoiceByInvoiceNumber(string invoiceNumber) => _invoice.Find(invoice => invoice.InvoiceNumber == invoiceNumber).FirstOrDefault();
        public List<Invoice> GetInvoiceByStatus(string invoiceStatus) => _invoice.Find(invoice => invoice.InvoiceStatus == invoiceStatus).ToList();
        public Invoice CreateInvoice(Invoice invoice)
        {
            _invoice.InsertOne(invoice);
            return invoice;
        }

        public void EditInvoice(string invoiceId, Invoice invoiceBody) => _invoice.ReplaceOne(invoice => invoice.InvoiceId == invoiceId, invoiceBody);
        public void DeleteInvoice(string invoiceId, Invoice invoiceBody) => _invoice.ReplaceOne(invoice => invoice.InvoiceId == invoiceId, invoiceBody);

    }
}

