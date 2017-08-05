using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;


namespace FITEvents.Classes
{
    public class Deliverable
    {
        public string deliverableID { get; set; }
        public string phaseID { get; set; }
        public string phaseName { get; set; }
        public string deliverableName { get; set; }
        public string vendorID { get; set; }
        public string vendorName { get; set; }
        public string team { get; set; }
        public string teamName { get; set; }
        public string priority { get; set; }
        public string notes { get; set; }

        public Deliverable()
        {

        }

        public Deliverable(string _deliverableID, string _phaseID, string _phaseName, string _deliverableName, string _vendorID, string _vendorName, string _team, string _teamName, string _priority, string _notes)
        {
            deliverableID = _deliverableID;
            phaseID = _phaseID;
            phaseName = _phaseName;
            deliverableName = _deliverableName;
            vendorID = _vendorID;
            vendorName = _vendorName;
            team = _team;
            teamName = _teamName;
            priority = _priority;
            notes = _notes;
        }

        public async void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/deliverable", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }

        public async Task<Deliverable> Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/deliverable", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);

            Deliverable deliverable = JsonConvert.DeserializeObject<Deliverable>(response.Content);
            return deliverable;
        }
        static public async Task<Deliverable> GetDeliverable(string deliverableID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/deliverable/" + deliverableID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            Deliverable deliverable = JsonConvert.DeserializeObject<Deliverable>(response.Content);

            return deliverable;
        }

        static public async Task<List<Deliverable>> GetAllDeliverables(string phaseID)
        {
            List<Deliverable> resultsAsList = new List<Deliverable>();

            var client = Globals.client;
            var request = new RestRequest("api/deliverable/getall/" + phaseID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            resultsAsList = JsonConvert.DeserializeObject<List<Deliverable>>(response.Content);

            return resultsAsList;

        }

        public async System.Threading.Tasks.Task Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/deliverable", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode); ;
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }
    }
}
