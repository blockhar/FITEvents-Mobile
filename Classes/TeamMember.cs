using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Android.Util;

namespace FITEvents.Classes
{
    public class TeamMember
    {
        public string teamMemberID { get; set; }
        public string teamID { get; set; }
        public string teamName { get; set; }
        public string teamMemberEmail { get; set; }
        public string teamMemberName { get; set; }
        public string teamMemberPhone { get; set; }

        public TeamMember()
        {

        }

        public TeamMember(string _teamMemberID, string _teamID, string _teamMemberEmail, string _teamMemberName, string _teamMemberPhone)
        {
            teamMemberID = _teamMemberID;
            teamID = _teamID;
            teamMemberEmail = _teamMemberEmail;
            teamMemberName = _teamMemberName;
            teamMemberPhone = _teamMemberPhone;
        }

        public async void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/teamMember", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }

        public async void Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/teamMember", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }
        static public async Task<TeamMember> GetTeamMember(string teamMemberID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/teamMember/" + teamMemberID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            TeamMember teamMember = JsonConvert.DeserializeObject<TeamMember>(response.Content);

            return teamMember;
        }

        static public async Task<List<TeamMember>> GetAllTeamMembers(string teamID)
        {
            List<TeamMember> resultsAsList = new List<TeamMember>();

            var client = Globals.client;
            var request = new RestRequest("api/teamMember/getall/" + teamID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            Log.Info("FITEVENTS", "Checking bearer code: " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);
            Log.Info("FITEVENTS", "Get all team members response: " + response.Content );

            resultsAsList = JsonConvert.DeserializeObject<List<TeamMember>>(response.Content);

            return resultsAsList;

        }

        static public async Task<List<TeamMember>> GetAllEventTeamMembers(string eventID)
        {
            List<TeamMember> resultsAsList = new List<TeamMember>();

            var client = Globals.client;
            var request = new RestRequest("api/teamMember/getallevent/" + eventID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            Log.Info("FITEVENTS", "Checking bearer code: " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);
            Log.Info("FITEVENTS", "Get all team members response: " + response.Content);

            resultsAsList = JsonConvert.DeserializeObject<List<TeamMember>>(response.Content);

            return resultsAsList;

        }

        public async void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/teamMember", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);

            IRestResponse response = await client.ExecuteTaskAsync(request);
            
        }
    }
}
