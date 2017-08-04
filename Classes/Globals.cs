using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Xamarin.Forms;

namespace FITEvents.Classes
{
    static class Globals
    {
        static public Event ActiveEvent;

        static public RestClient client = new RestClient("https://eventapp.forecheckit.ca");

        static public string clientID = "";

        static public string BearerCode;

        static public User loggedInUser;
    }
    
    static class Cache
    {
        static public List<Task> taskList;
    }

    static class FITEventStyles
    {
        static public Style buttonStyle = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter {Property = Button.BackgroundColorProperty, Value = Color.FromRgb(241,184,45) },
                new Setter {Property = Button.TextColorProperty, Value = Color.White },
                new Setter {Property = View.MarginProperty, Value = new Thickness(10)}
            }
        };

        static public Style entryStyle = new Style(typeof(Entry))
        {
            Setters =
            {
                new Setter {Property = Entry.TextColorProperty, Value = Color.White},
                new Setter {Property = Entry.PlaceholderColorProperty, Value = Color.FromRgb(173,173,173)}
            }
        };

        static public Style labelStyle = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter {Property = Label.TextColorProperty, Value = Color.FromRgb(241,184,45)}
            }
        };
    }
}
