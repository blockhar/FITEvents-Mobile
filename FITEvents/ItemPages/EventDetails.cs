﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        ActivityIndicator spinner;

        Event myEvent;

        public EventDetails(Event _myEvent)
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

            myEvent = _myEvent;

            btnPhases = new Button { Text = "View Phases" };
            btnPhases.Clicked += OnbtnPhasesClick;
            lbleventName = new Label { Text = "Event Name" };
            enteventName = new Entry { Text = myEvent.eventName };
            lbleventLocation = new Label { Text = "Event Location" };
            enteventLocation = new Entry { Text = myEvent.eventLocation };
            lbleventDate = new Label { Text = "Event Date" };
            //If event has no date (i.e. is being created) default to current date.
            enteventDate = new DatePicker { Date = (myEvent.eventDate == DateTime.MinValue ? DateTime.Now : myEvent.eventDate) };
            btnSaveClose = new Button { Text = "Save and Close" };
            btnSaveClose.Clicked += Save;
            btnSaveClose.Clicked += Close;
            btnSave = new Button { Text = "Save" };
            btnSave.Clicked += Save;
            btnDelete = new Button { Text = "Delete" };
            btnDelete.Clicked += OnbtnDeleteClick;
            spinner = new ActivityIndicator();

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
                    btnDelete,
                    spinner
                }
            };

            this.Content = new ScrollView
            {
                Content = stack
            };
        }

        async void Save(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            myEvent.eventName = enteventName.Text;
            myEvent.eventLocation = enteventLocation.Text;
            myEvent.eventDate =  enteventDate.Date;

            if (String.IsNullOrEmpty(myEvent.eventID))
            {
                Log.Info("FITEVENTS","Creating Event");
                myEvent = await myEvent.Create();
                await Navigation.PushModalAsync(new listPhases(myEvent));
            }
            else
            {
                Log.Info("FITEVENTS", "Saving Event. ID is: " + myEvent.eventID.ToString());
                myEvent.Save();
            }
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        void Close (object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void OnbtnDeleteClick(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            myEvent.Delete();
            Navigation.PopModalAsync();
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        async void OnbtnPhasesClick(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            
            await Navigation.PushModalAsync(new listPhases(myEvent));
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }
    }
}
