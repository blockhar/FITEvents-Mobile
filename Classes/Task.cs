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

        public void Save()
        {
            var client = Globals.client;
            var request = new RestRequest("api/task", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            //this.dueDate = this.dueDate.ToUniversalTime();
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            //response.StatusCode
        }

        public Task Create()
        {
            var client = Globals.client;
            var request = new RestRequest("api/task", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            Task task = JsonConvert.DeserializeObject<Task>(response.Content);

            return task;
            //response.StatusCode
        }
        static public Task GetTask(string taskID)
        {
            var client = Globals.client;
            var request = new RestRequest("api/task/" + taskID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = client.Execute(request);

            Task task = JsonConvert.DeserializeObject<Task>(response.Content);

            return task;
        }

        static public List<Task> GetAllTasks(string deliverableID)
        {
            List<Task> resultsAsList = new List<Task>();

            var client = Globals.client;
            var request = new RestRequest("api/task/getall/" + deliverableID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = client.Execute(request);

            resultsAsList = JsonConvert.DeserializeObject<List<Task>>(response.Content);

            return resultsAsList;

        }

        //GetAllEventTasks
        static public List<Task> GetAllEventTasks(string eventID)
        {
            List<Task> resultsAsList = new List<Task>();

            var client = Globals.client;
            var request = new RestRequest("api/task/getallevent/" + eventID, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);

            IRestResponse response = client.Execute(request);

            resultsAsList = JsonConvert.DeserializeObject<List<Task>>(response.Content);

            return resultsAsList;

        }

        public void Delete()
        {
            var client = Globals.client;
            var request = new RestRequest("api/task", Method.DELETE);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Globals.BearerCode);
            request.AddJsonBody(this);


            IRestResponse response = client.Execute(request);

            //response.StatusCode
        }
    }
}
