using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;

using DMSapi_v2.Models;
namespace DMSapi_v2.Services
{
    public class PaymentService
    {
        private readonly IMongoCollection<Payment> _payment;

        public PaymentService(DatabaseSetting setting)
        {
            var Client = new MongoClient(setting.ConnectionString);
            var database = Client.GetDatabase(setting.DatabaseName);
            var filter = Builders<Payment>.Filter.Where(payment => payment.Status == "Open");
            _payment = database.GetCollection<Payment>(setting.PaymentCollection);
        }

        public List<Payment> GetAllPayment() => _payment.Find(payment => payment.Status == "Open").ToList();
        public List<Payment> GetAllPaymentForApi() => _payment.Find(payment => true).ToList();
        public Payment GetPaymentById(string paymentId) => _payment.Find(payment => payment.PaymentId == paymentId).FirstOrDefault();
        public Payment GetPaymentByPaymentNumber(string paymentNumber) => _payment.Find(payment => payment.PaymentNumber == paymentNumber).FirstOrDefault();
        public List<Payment> GetPaymentByStatus(string paymentStatus) => _payment.Find(payment => payment.PaymentStatus == paymentStatus).ToList();
        public Payment CreatePayment(Payment payment)
        {
            _payment.InsertOne(payment);
            return payment;
        }

        public void EditPayment(string paymentId, Payment paymentBody) => _payment.ReplaceOne(payment => payment.PaymentId == paymentId, paymentBody);
        public void DeletePayment(string paymentId, Payment paymentBody) => _payment.ReplaceOne(payment => payment.PaymentId == paymentId, paymentBody);

    }
}

