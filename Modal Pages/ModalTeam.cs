using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
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

        public ModalTeam(DeliverableDetails _deliverablePage, List<Team> allTeams)
        {
            deliverablePage = _deliverablePage;

            btnNewTeam = new Button { Text = "Create New Team" };
            btnNewTeam.Clicked += OnbtnNewTeamClick;

            listView = new ListView
            {

                // Source of data items.
                ItemsSource = allTeams,
                HasUnevenRows = true,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property

                    Label teamLabel = new Label();
                    teamLabel.SetBinding(Label.TextProperty, "teamName");

                    // Return an assembled ViewCell.
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

            Team selectedTeam = (Team)e.SelectedItem;
            deliverablePage.updateTeam(selectedTeam);
            Navigation.PopModalAsync();

        }

        void OnbtnNewTeamClick(object sender, EventArgs e)
        {
            Team newTeam = new Team();
            newTeam.eventID = Globals.ActiveEvent.eventID;
            newTeam.eventName = Globals.ActiveEvent.eventName;
            Navigation.PushModalAsync(new TeamDetails(newTeam)); ;
        }
    }
}
