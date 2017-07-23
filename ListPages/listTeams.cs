using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;

using Xamarin.Forms;

namespace FITEvents.ListPages
{
    public class listTeams : ContentPage
    {

        ListView listView;
        Button btnNewTeam;
        Event myEvent;
        ActivityIndicator spinner;

        public listTeams(List<Team> allTeams, Event _myEvent)
        {
            myEvent = _myEvent;
            btnNewTeam = new Button { Text = "Create New Team" };
            btnNewTeam.Clicked += OnbtnNewTeamClick;
            spinner = new ActivityIndicator();

            listView = new ListView
            {

                ItemsSource = allTeams,
                HasUnevenRows = true,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "teamName");

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
                    btnNewTeam,
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
            Team selectedTeam = (Team)e.SelectedItem;
            Navigation.PushModalAsync(new TeamDetails(selectedTeam));
            ((ListView)sender).SelectedItem = null;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

        }

        void OnbtnNewTeamClick(object sender, EventArgs e)
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            Team newTeam = new Team();
            newTeam.eventID = myEvent.eventID;
            newTeam.eventName = myEvent.eventName;
            Navigation.PushModalAsync(new TeamDetails(newTeam));
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }
    }
}
