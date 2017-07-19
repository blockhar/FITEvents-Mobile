using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using FITEvents.Classes;
using FITEvents.ListPages;

using Xamarin.Forms;
using Android.Util;

namespace FITEvents.ItemPages
{
    public class EventDetails : ContentPage
    {
        Button btnPhases;
        Label lbleventName;
        Label lbleventLocation;
        Label lbleventDate;
        Entry enteventName;
        Entry enteventLocation;
        DatePicker enteventDate;
        Button btnSaveClose;
        Button btnSave;
        Button btnDelete;

        Event myEvent;

        public EventDetails(Event _myEvent)
        {
            myEvent = _myEvent;

            btnPhases = new Button { Text = "View Phases" };
            btnPhases.Clicked += OnbtnPhasesClick;
            lbleventName = new Label { Text = "Event Name" };
            enteventName = new Entry { Text = myEvent.eventName };
            lbleventLocation = new Label { Text = "Event Location" };
            enteventLocation = new Entry { Text = myEvent.eventLocation };
            lbleventDate = new Label { Text = "Event Date" };
            //enteventDate = new DatePicker { Date = myEvent.eventDate };
            enteventDate = new DatePicker { Date = (myEvent.eventDate == DateTime.MinValue ? DateTime.Now : myEvent.eventDate) };
            btnSaveClose = new Button { Text = "Save and Close" };
            btnSaveClose.Clicked += Save;
            btnSaveClose.Clicked += Close;
            btnSave = new Button { Text = "Save" };
            btnSave.Clicked += Save;
            btnDelete = new Button { Text = "Delete" };
            btnDelete.Clicked += OnbtnDeleteClick;

            StackLayout stack = new StackLayout
            {
                Children = {
                    btnPhases,
                    lbleventName,
                    enteventName,
                    lbleventLocation,
                    enteventLocation,
                    lbleventDate,
                    enteventDate,
                    btnSaveClose,
                    btnSave,
                    btnDelete
                }
            };

            this.Content = new ScrollView
            {
                Content = stack
            };
        }

        async void Save(object sender, EventArgs e)
        {
            myEvent.eventName = enteventName.Text;
            myEvent.eventLocation = enteventLocation.Text;
            myEvent.eventDate =  enteventDate.Date;

            if (String.IsNullOrEmpty(myEvent.eventID))
            {
                Log.Info("FITEVENTS","Creating Event");
                myEvent = await myEvent.Create();
                await Navigation.PushModalAsync(new listPhases(new List<Phase>(), myEvent));
            }
            else
            {
                Log.Info("FITEVENTS", "Saving Event. ID is: " + myEvent.eventID.ToString());
                myEvent.Save();
            }
        }

        void Close (object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void OnbtnDeleteClick(object sender, EventArgs e)
        {

        }

        async void OnbtnPhasesClick(object sender, EventArgs e)
        {
            List<Phase> allPhases = await Phase.GetAllPhases(myEvent.eventID);
            await Navigation.PushModalAsync(new listPhases(allPhases, myEvent));
        }
    }
}
