using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace FITEvents.Classes
{
    public class Task
    {
        public string taskName { get; set; }
        public string taskID { get; set; }
        public string deliverableID { get; set; }
        public string deliverableName { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime dueDateLocal { get { return dueDate.ToLocalTime(); } set { } }
        public string assignedTo { get; set; }
        public string assignedToName { get; set; }
        public string completedBy { get; set; }
        public string completedByName { get; set; }
        public string notes { get; set; }

        public Task()
        {

        }

        public Task(string _taskName, string _taskID, string _deliverableID, string _deliverableName, DateTime _dueDate, string _assignedTo, string _assignedToName, string _completedBy, string _completedByName, string _notes)
        {
            taskName = _taskName;
            taskID = _taskID;
            deliverableID = _deliverableID;
            deliverableName = _deliverableName;
            dueDate = _dueDate;
            assignedTo = _assignedTo;
            assignedToName = _assignedToName;
            completedBy = _completedBy;
            completedByName = _completedByName;
            notes = _notes;
        }

        public async void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/task", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);

            //response.StatusCode
        }

        public async Task<Task> Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/task", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);

            Task task = JsonConvert.DeserializeObject<Task>(response.Content);

            return task;
            //response.StatusCode
        }
        static public async Task<Task> GetTask(string taskID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/task/" + taskID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            Task task = JsonConvert.DeserializeObject<Task>(response.Content);

            return task;
        }

        static public async Task<List<Task>> GetAllTasks(string deliverableID)
        {
            List<Task> resultsAsList = new List<Task>();

            var client = Globals.client;
            var request = new RestRequest("api/task/getall/" + deliverableID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            resultsAsList = JsonConvert.DeserializeObject<List<Task>>(response.Content);

            return resultsAsList;

        }

        //GetAllEventTasks
        static public async Task<List<Task>> GetAllEventTasks(string eventID)
        {
            List<Task> resultsAsList = new List<Task>();

            var client = Globals.client;
            var request = new RestRequest("api/task/getallevent/" + eventID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = await client.ExecuteTaskAsync(request);

            resultsAsList = JsonConvert.DeserializeObject<List<Task>>(response.Content);

            return resultsAsList;

        }

        public async void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/task", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = await client.ExecuteTaskAsync(request);

            //response.StatusCode
        }
    }
}
