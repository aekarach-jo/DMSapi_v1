using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;

using DMSapi_v2.Models;
namespace DMSapi_v2.Services
{
    public class CheckoutService
    {
        private readonly IMongoCollection<Checkout> _checkout;

        public CheckoutService(DatabaseSetting setting)
        {
            var Client = new MongoClient(setting.ConnectionString);
            var database = Client.GetDatabase(setting.DatabaseName);
            var filter = Builders<Checkout>.Filter.Where(checkout => checkout.Status == "Open");
            _checkout = database.GetCollection<Checkout>(setting.CheckoutCollection);
        }

        public List<Checkout> GetAllCheckout() => _checkout.Find(checkout => checkout.Status == "Open").ToList();
        public List<Checkout> GetAllCheckoutForApi() => _checkout.Find(checkout => true).ToList();
        public Checkout GetCheckoutById(string checkoutId) => _checkout.Find(checkout => checkout.CheckoutId == checkoutId).FirstOrDefault();
        public List<Checkout> GetCheckoutByStatus(string checkoutStatus) => _checkout.Find(checkout => checkout.CheckoutStatus == checkoutStatus).ToList();
         public List<Checkout> FilterCheckoutByMonth(DateTime datex1, DateTime datex2)
        {
             List<Checkout> data = _checkout.Find(s => s.CreationDateTime >= datex1 & s.CreationDateTime < datex2.AddDays(+1)).ToList();
            List<Checkout> filterStatus = data.FindAll(ss => ss.Status == "Open").ToList();
            return filterStatus;
        }


        public Checkout CreateCheckout(Checkout checkout)
        {
            _checkout.InsertOne(checkout);
            return checkout;
        }

        public void EditCheckout(string checkoutId, Checkout checkoutBody) => _checkout.ReplaceOne(checkout => checkout.CheckoutId == checkoutId, checkoutBody);
        public void DeleteCheckout(string checkoutId, Checkout checkoutBody) => _checkout.ReplaceOne(checkout => checkout.CheckoutId == checkoutId, checkoutBody);

    }
}

