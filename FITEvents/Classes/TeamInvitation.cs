using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace FITEvents.Classes
{
    public class TeamInvitation
    {
        public string teamID { get; set; }
        public string teamName { get; set; }
        public string eventID { get; set; }
        public string eventName { get; set; }
        public string inviteEmail { get; set; }
        public string status { get; set; }

        public TeamInvitation()
        {

        }

        public TeamInvitation(string _teamID, string _inviteEmail, string _status)
        {
            teamID = _teamID;
            inviteEmail = _inviteEmail;
            status = _status;

        }

        public async void Accept()
        {
            this.status = "Accepted";
            var client = Globals.client;
            var request = new RestRequest("api/teaminvitation/accept", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);
            
            IRestResponse response = await client.ExecuteTaskAsync(request);
        }

        public async void Decline()
        {
            this.status = "Declined";
            var client = Globals.client;
            var request = new RestRequest("api/teaminvitation/decline", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }

        public async Task<TeamInvitation> Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/teaminvitation", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            TeamInvitation teamInvitation = JsonConvert.DeserializeObject<TeamInvitation>(response.Content);
            return teamInvitation;
        }
        static public async Task<TeamInvitation> GetAllTeamInvites(string teamID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/teaminvitation/getallteam/" + teamID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            TeamInvitation team = JsonConvert.DeserializeObject<TeamInvitation>(response.Content);

            return team;
        }

        static public async Task<List<TeamInvitation>> GetAllEventInvites(string eventID)
        {
            List<TeamInvitation> resultsAsList = new List<TeamInvitation>();

            var client = Globals.client;
            var request = new RestRequest("api/teaminvitation/getallevent/" + eventID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            resultsAsList = JsonConvert.DeserializeObject<List<TeamInvitation>>(response.Content);

            return resultsAsList;

        }

         static public async Task<List<TeamInvitation>> GetAllEmailInvites()
        {
            List<TeamInvitation> resultsAsList = new List<TeamInvitation>();

            var client = Globals.client;
            var request = new RestRequest("api/teaminvitation/getallemail/", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            resultsAsList = JsonConvert.DeserializeObject<List<TeamInvitation>>(response.Content);

            return resultsAsList;

        }

        public async void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/teaminvitation", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);
        }
    }
}
