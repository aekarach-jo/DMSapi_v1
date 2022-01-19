using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;

using DMSapi_v2.Models;
namespace DMSapi_v2.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(DatabaseSetting setting)
        {
            var Client = new MongoClient(setting.ConnectionString);
            var database = Client.GetDatabase(setting.DatabaseName);
            var filter = Builders<User>.Filter.Where(user => user.Status == "Open");
            _user = database.GetCollection<User>(setting.UserCollection);
        }

        public List<User> GetAllUser() => _user.Find(user => user.Status == "Open").ToList();
        public List<User> GetAllUserForApi() => _user.Find(user => true).ToList();
        public User GetUserById(string userId) => _user.Find(user => user.UserId == userId).FirstOrDefault();
        public User GetUserByUserName(string userName) => _user.Find(user => user.UserName == userName).FirstOrDefault();
        public User GetUserByPermission(string permission) => _user.Find(user => user.Permission == permission).FirstOrDefault();
        public User CreateUser(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public void EditUser(string userId, User userBody) => _user.ReplaceOne(user => user.UserId == userId, userBody);
        public void DeleteUser(string userId, User userBody) => _user.ReplaceOne(user => user.UserId == userId, userBody);

        public User CheckUserName(string userName) => _user.Find<User>(user => user.UserName == userName).FirstOrDefault();
        public User CheckUserNameAndPassword(string username,string  password) => _user.Find<User>(user => user.UserName == username && user.Password == password && user.Status == "Open").FirstOrDefault();
    }
}

