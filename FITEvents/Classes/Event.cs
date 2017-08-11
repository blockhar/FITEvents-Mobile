using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using Newtonsoft.Json;
using Android.Util;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace FITEvents.Classes
{
    public class Event
    {
        public string eventID { get; set; }
        public string eventName { get; set; }
        public DateTime eventDate { get; set; }
        public DateTime eventDateLocal { get { return eventDate.ToLocalTime(); } set { } }
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

        public async void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/event", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }

        public async Task<Event> Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/event", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
            Event myEvent = JsonConvert.DeserializeObject<Event>(response.Content);
            return myEvent;
        }
        static public async Task<Event> GetEvent(string eventID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/event/" + eventID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            Event myevent = JsonConvert.DeserializeObject<Event>(response.Content);

            return myevent;
        }


        static public async Task<List<Event>> GetAllEvents()
        {
            List<Event> resultsAsList = new List<Event>();

            var client = Globals.client;
            var request = new RestRequest("api/event/getall/", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);
            Log.Info("FITEVENTS", "Got my all events response. It is " + response.Content);

            if ((int)response.StatusCode == 200)
            {
                resultsAsList = JsonConvert.DeserializeObject<List<Event>>(response.Content);
            }

            else
            {
                Log.Info("FITEVENTS", "Error.Http Status: " + response.StatusCode + ".Error Message: " + response.Content);
                throw new Exception("Error.Http Status: " + response.StatusCode + ".Error Message: " + response.Content);
            }
            

            return resultsAsList;

        }

        public async void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/event", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }
    }
}
