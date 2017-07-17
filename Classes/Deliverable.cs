﻿using System;
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
        public int priority { get; set; }
        public string notes { get; set; }

        public Deliverable()
        {

        }

        public Deliverable(string _deliverableID, string _phaseID, string _phaseName, string _deliverableName, string _vendorID, string _vendorName, string _team, string _teamName, int _priority, string _notes)
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

        public void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/deliverable", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            //response.StatusCode
        }

        public Deliverable Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/deliverable", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            Deliverable deliverable = JsonConvert.DeserializeObject<Deliverable>(response.Content);
            return deliverable;
            //response.StatusCode
        }
        static public Deliverable GetDeliverable(string deliverableID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/deliverable/" + deliverableID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = client.Execute(request);

            Deliverable deliverable = JsonConvert.DeserializeObject<Deliverable>(response.Content);

            return deliverable;
        }

        static public List<Deliverable> GetAllDeliverables(string phaseID)
        {
            List<Deliverable> resultsAsList = new List<Deliverable>();

            var client = Globals.client;
            var request = new RestRequest("api/deliverable/getall/" + phaseID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = client.Execute(request);

            resultsAsList = JsonConvert.DeserializeObject<List<Deliverable>>(response.Content);

            return resultsAsList;

        }

        public void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/deliverable", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode); ;
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            //response.StatusCode
        }
    }
}