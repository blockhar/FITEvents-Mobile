using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace FITEvents.Classes
{
    public class Phase
    {
        public string phaseID { get; set; }
        public string eventID { get; set; }
        public string eventName { get; set; }
        public string phaseName { get; set; }
        public int phaseOrder { get; set; }

        public Phase()
        {

        }

        public Phase(string _phaseID, string _eventID, string _eventName, string _phaseName, int _phaseOrder)
        {
            phaseID = _phaseID;
            eventID = _eventID;
            eventName = _eventName;
            phaseName = _phaseName;
            phaseOrder = _phaseOrder;
        }

        public async void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/phase", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);

            //response.StatusCode
        }

        public async Task<Phase> Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/phase", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);

            Phase phase = JsonConvert.DeserializeObject<Phase>(response.Content);
            return phase;
            //response.StatusCode
        }
        static public async Task<Phase> GetPhase(string phaseID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/phase/" + phaseID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            Phase phase = JsonConvert.DeserializeObject<Phase>(response.Content);

            return phase;
        }

        static public async Task<List<Phase>> GetAllPhases(string eventID)
        {
            List<Phase> resultsAsList = new List<Phase>();

            var client = Globals.client;
            var request = new RestRequest("api/phase/getall/" + eventID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            resultsAsList = JsonConvert.DeserializeObject<List<Phase>>(response.Content);

            return resultsAsList;

        }

        public async void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/phase", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);

            //response.StatusCode
        }
    }
}
