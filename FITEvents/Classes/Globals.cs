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

        static public Style pageStyle = new Style(typeof(ContentPage))
        {
            Setters =
            {
                new Setter {Property = ContentPage.BackgroundImageProperty, Value = "h960w576.png"}
            }
        };

        static public Style pickerStyle = new Style(typeof(Picker))
        {
            Setters =
            {
                new Setter {Property = Picker.TextColorProperty, Value = Color.White}
            }
        };

        static public Style datePickerStyle = new Style(typeof(DatePicker))
        {
            Setters =
            {
                new Setter {Property = DatePicker.TextColorProperty, Value = Color.White}
            }
        };

        static public Style timePickerStyle = new Style(typeof(TimePicker))
        {
            Setters =
            {
                new Setter {Property = TimePicker.TextColorProperty, Value = Color.White}
            }
        };

        static public string GlobalBGImageName = "h960w576.jpg";
    }
}
