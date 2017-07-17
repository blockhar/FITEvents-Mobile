using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Android.Util;

namespace FITEvents.Classes
{
    public class User
    {
        public string userID { get; set; }
        public string userEmail { get; set; }
        public string userName { get; set; }
        public string userPhone { get; set; }

        public User()
        {

        }

        public User(string _userID, string _userEmail, string _userName, string _userPhone)
        {
            userID = _userID;
            userEmail = _userEmail;
            userName = _userName;
            userPhone = _userPhone;
        }

        public void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/user", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            //response.StatusCode
        }

        public User Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/user/", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            Log.Info("FITEVENTS", "User Create response: " + response.Content);

            User user = JsonConvert.DeserializeObject<User>(response.Content);
            return user;
        }
        static public User GetUser(string userID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/user/" + userID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = client.Execute(request);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            return user;
        }

        static public User GetLoggedInUser()
        {
            var client = Globals.client;
            var request = new RestRequest("api/user/", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = client.Execute(request);

            Log.Info("FITEVENTS", "User Get Logged In User response: " + response.Content);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            return user;
        }

        static public List<User> GetAllUsers(string eventID)
        {
            List<User> resultsAsList = new List<User>();

            var client = Globals.client;
            var request = new RestRequest("api/user/getall/" + eventID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            IRestResponse response = client.Execute(request);

            resultsAsList = JsonConvert.DeserializeObject<List<User>>(response.Content);

            return resultsAsList;

        }

        public void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/user", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            //response.StatusCode
        }
    }
}
