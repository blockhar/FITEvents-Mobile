using System;
using System.Collections.Generic;
using System.Linq;
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
        ActivityIndicator spinner;

        public listEvents(List<Event> allEvents = null)
        {
            btnNewEvent = new Button { Text = "Create New Event" };
            btnNewEvent.Clicked += OnbtnNewEventClick;
            spinner = new ActivityIndicator();

            listView = new ListView
            {

                ItemsSource = allEvents,
                HasUnevenRows = true,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "eventName");

                    Label locationLabel = new Label();
                    locationLabel.SetBinding(Label.TextProperty, "eventLocatiton");

                    Label dueDateLabel = new Label();
                    dueDateLabel.SetBinding(Label.TextProperty, "eventDate");

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

            Event selectedEvent = (Event)e.SelectedItem;
            Globals.ActiveEvent = selectedEvent;
            Navigation.PushModalAsync(new EventHome(selectedEvent));

            ((ListView)sender).SelectedItem = null;
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }

        void OnbtnNewEventClick(object sender, EventArgs e)
        {
            Event newEvent = new Event();
            Navigation.PushModalAsync(new EventDetails(newEvent));
        }
    }
}
