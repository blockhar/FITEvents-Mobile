using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;
using FITEvents.HomePages;

using Xamarin.Forms;

namespace FITEvents.ListPages
{
    public class listEvents : ContentPage
    {
        
        ListView listView;
        Button btnNewEvent;

        public listEvents(List<Event> allEvents = null)
        {
            btnNewEvent = new Button { Text = "Create New Event" };
            btnNewEvent.Clicked += OnbtnNewEventClick;

            listView = new ListView
            {

                // Source of data items.
                ItemsSource = allEvents,
                HasUnevenRows = true,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "eventName");

                    Label locationLabel = new Label();
                    locationLabel.SetBinding(Label.TextProperty, "eventLocatiton");

                    Label dueDateLabel = new Label();
                    dueDateLabel.SetBinding(Label.TextProperty, "eventDate");

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Children =
                            {
                                nameLabel,
                                locationLabel,
                                dueDateLabel
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
                    btnNewEvent,
                    listView
                }
            };
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            Event selectedEvent = (Event)e.SelectedItem;
            Globals.ActiveEvent = selectedEvent;
            Navigation.PushModalAsync(new EventHome(selectedEvent));

            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }

        void OnbtnNewEventClick(object sender, EventArgs e)
        {
            Event newEvent = new Event();
            Navigation.PushModalAsync(new EventDetails(newEvent));
        }
    }
}
