using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;

using Xamarin.Forms;

namespace FITEvents.ModalPages
{
    public class ModalTeam : ContentPage
    {

        ListView listView;
        Button btnNewTeam;
        DeliverableDetails deliverablePage;
        ActivityIndicator spinner;

        public ModalTeam(DeliverableDetails _deliverablePage, List<Team> allTeams)
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

            deliverablePage = _deliverablePage;

            btnNewTeam = new Button { Text = "Create New Team" };
            btnNewTeam.Clicked += OnbtnNewTeamClick;
            spinner = new ActivityIndicator();

            listView = new ListView
            {

                ItemsSource = allTeams,
                HasUnevenRows = true,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label teamLabel = new Label();
                    teamLabel.SetBinding(Label.TextProperty, "teamName");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Children =
                            {
                                teamLabel
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
            deliverablePage.updateTeam(selectedTeam);
            Navigation.PopModalAsync();
            spinner.IsRunning = false;
            spinner.IsVisible = false;

        }

        void OnbtnNewTeamClick(object sender, EventArgs e)
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            Team newTeam = new Team();
            newTeam.eventID = Globals.ActiveEvent.eventID;
            newTeam.eventName = Globals.ActiveEvent.eventName;
            Navigation.PushAsync(new TeamDetails(newTeam)); ;
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }
    }
}
