using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;

using DMSapi_v2.Models;
namespace DMSapi_v2.Services
{
    public class UserDetailService
    {
        private readonly IMongoCollection<UserDetail> _userDetail;

        public UserDetailService(DatabaseSetting setting)
        {
            var Client = new MongoClient(setting.ConnectionString);
            var database = Client.GetDatabase(setting.DatabaseName);
            var filter = Builders<UserDetail>.Filter.Where(userDetail => userDetail.Status == "Open");
            _userDetail = database.GetCollection<UserDetail>(setting.UserDetailCollection);
        }

        public List<UserDetail> GetAllUserDetail() => _userDetail.Find(userDetail => userDetail.Status == "Open").ToList();
        public List<UserDetail> GetAllUserDetailForApi() => _userDetail.Find(userDetail => true).ToList();
        public UserDetail GetUserDetailById(string userDetailId) => _userDetail.Find(userDetail => userDetail.UserDetailId == userDetailId).FirstOrDefault();
        public UserDetail GetUserDetailByFirstName(string firstName) => _userDetail.Find(userDetail => userDetail.FirstName == firstName).FirstOrDefault();
        public UserDetail GetUserDetailByTelephone(string tel) => _userDetail.Find(userDetail => userDetail.Tel == tel).FirstOrDefault();
        public UserDetail CreateUserDetail(UserDetail userDetail)
        {
            _userDetail.InsertOne(userDetail);
            return userDetail;
        }

        public void EditUserDetail(string userDetailId, UserDetail userDetailBody) => _userDetail.ReplaceOne(userDetail => userDetail.UserDetailId == userDetailId, userDetailBody);
        public void DeleteUserDetail(string userDetailId, UserDetail userDetailBody) => _userDetail.ReplaceOne(userDetail => userDetail.UserDetailId == userDetailId, userDetailBody);

      }
}

