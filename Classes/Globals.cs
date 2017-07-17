using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace FITEvents.Classes
{
    static class Globals
    {
        static public Event ActiveEvent;

        static public RestClient client = new RestClient("https://eventapp.forecheckit.ca");

        static public string clientID = "";

        static public string BearerCode;

        //static public string loggedInEmail;

        static public User loggedInUser;
    }
    
    static class Cache
    {
        static public List<Task> taskList;
    }

}
