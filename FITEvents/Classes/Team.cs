﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Android.Util;

namespace FITEvents.Classes
{
    public class Team
    {
        public string teamID { get; set; }
        public string eventID { get; set; }
        public string eventName { get; set; }
        public string teamName { get; set; }

        public Team()
        {

        }

        public Team(string _teamID, string _eventID, string _eventName, string _teamName)
        {
            teamID = _teamID;
            eventID = _eventID;
            eventName = _eventName;
            teamName = _teamName;
        }

        public async void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/team", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }

        public async Task<Team> Create()
        {
            Log.Info("FITEVENTS", "Entered Team.cs create method.");
            var client = Globals.client;
            var request = new RestRequest("api/team", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);
            Log.Info("FITEVENTS", "team.cs create Sending request: "+ request.ToString());
            
            Log.Info("FITEVENTS", "team.cs create Sending request");
            IRestResponse response = await client.ExecuteTaskAsync(request);
            Log.Info("FITEVENTS", "team.cs create received response. it is: " + response.Content);

            Team team = JsonConvert.DeserializeObject<Team>(response.Content);
            return team;
        }

        static public async Task<Team> GetTeam(string teamID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/team/" + teamID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            Team team = JsonConvert.DeserializeObject<Team>(response.Content);

            return team;
        }

        static public async Task<List<Team>> GetAllTeams(string eventID)
        {
            List<Team> resultsAsList = new List<Team>();

            var client = Globals.client;
            var request = new RestRequest("api/team/getall/" + eventID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            resultsAsList = JsonConvert.DeserializeObject<List<Team>>(response.Content);

            return resultsAsList;

        }

        public async void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/team", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }
    }
}
