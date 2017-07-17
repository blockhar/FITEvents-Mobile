using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using Newtonsoft.Json;
using Android.Util;
using Xamarin.Forms;

namespace FITEvents.Classes
{
    public class Event
    {
        public string eventID { get; set; }
        public string eventName { get; set; }
        public DateTime eventDate { get; set; }
        public string eventLocation { get; set; }
        public string eventAdmin { get; set; }

        public Event()
        {

        }

        public Event(string _eventID, string _eventName, DateTime _eventDate, string _eventLocation, string _eventAdmin)
        {
            eventID = _eventID;
            eventName = _eventName;
            eventDate = _eventDate;
            eventLocation = _eventLocation;
            eventAdmin = _eventAdmin;
        }

        public void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/event", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            //response.StatusCode
        }

        public Event Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/event", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);
            Event myEvent = JsonConvert.DeserializeObject<Event>(response.Content);
            return myEvent;

            //response.StatusCode
        }
        static public Event GetEvent(string eventID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/event/" + eventID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = client.Execute(request);

            Event myevent = JsonConvert.DeserializeObject<Event>(response.Content);

            return myevent;
        }


        static public List<Event> GetAllEvents()
        {
            List<Event> resultsAsList = new List<Event>();

            var client = Globals.client;
            var request = new RestRequest("api/event/getall/", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = client.Execute(request);
            Log.Info("FITEVENTS", "Got my all events response. It is " + response.Content);

            if ((int)response.StatusCode == 200)
            {
                resultsAsList = JsonConvert.DeserializeObject<List<Event>>(response.Content);
            }

            else
            {
                //DisplayAlert("Error", "Error. Http Status: " + response.StatusCode + ". Error Message: " + response.Content, "OK");
                Log.Info("FITEVENTS", "Error.Http Status: " + response.StatusCode + ".Error Message: " + response.Content);
                throw new Exception("Error.Http Status: " + response.StatusCode + ".Error Message: " + response.Content);
            }
            

            return resultsAsList;

        }

        public void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/event", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            //response.StatusCode
        }
    }
}
