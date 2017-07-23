﻿using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;

using Xamarin.Forms;

namespace FITEvents.ListPages
{
    public class listPhases : ContentPage
    {

        ListView listView;
        Button btnNewPhase;
        Event myEvent;
        ActivityIndicator spinner;

        public listPhases(List<Phase> allPhases, Event _myEvent)
        {
            myEvent = _myEvent;
            btnNewPhase = new Button { Text = "Create New Phase" };
            btnNewPhase.Clicked += OnbtnNewPhaseClick;
            spinner = new ActivityIndicator();

            listView = new ListView
            {

                // Source of data items.
                ItemsSource = allPhases,
                HasUnevenRows = true,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "phaseName");

                    //Label orderLabel = new Label();
                    //orderLabel.SetBinding(Label.TextProperty, "phaseOrder");

                    //Label dueDateLabel = new Label();
                    //dueDateLabel.SetBinding(Label.TextProperty, "eventDate");

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Children =
                            {
                                nameLabel
                            }
                        }
                    };
                })
            };

            listView.ItemSelected += OnSelection;

            Content = new StackLayout
            {
                Children =
                {
                    btnNewPhase,
                    listView,
                    spinner
                }
            };
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            Phase selectedPhase = (Phase)e.SelectedItem;
            Navigation.PushModalAsync(new PhaseDetails(selectedPhase));
            ((ListView)sender).SelectedItem = null;
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }

        void OnbtnNewPhaseClick(object sender, EventArgs e)
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            Phase newPhase = new Phase();
            newPhase.eventID = myEvent.eventID;
            newPhase.eventName = myEvent.eventName;
            Navigation.PushModalAsync(new PhaseDetails(newPhase));
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }
    }
}
