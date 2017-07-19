using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;
using FITEvents.ModalPages;
using Xamarin.Forms;

namespace FITEvents.ListPages
{
    public class listTeamMembers : ContentPage
    {
        ListView listView;
        Button btnNewTeamMember;
        Team team;

        public listTeamMembers(List<TeamMember> allTeamMembers, Team _team)
        {
            team = _team;
            btnNewTeamMember = new Button { Text = "Add TeamMember" };
            btnNewTeamMember.Clicked += OnbtnNewTeamMemberClick;

            listView = new ListView
            {

                // Source of data items.
                ItemsSource = allTeamMembers,
                HasUnevenRows = true,


                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "teamMemberName");

                    Label teamLabel = new Label();
                    teamLabel.SetBinding(Label.TextProperty, "teamName");

                    Label emailLabel = new Label();
                    emailLabel.SetBinding(Label.TextProperty, "teamMemberEmail");

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
                                teamLabel,
                                emailLabel
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
                    btnNewTeamMember,
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

            TeamMember selectedTeamMember = (TeamMember)e.SelectedItem;
            //Navigation.PushModalAsync(new TeamMemberDetails(selectedTeamMember));

            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }

        private async void OnbtnNewTeamMemberClick(object sender, EventArgs e)
        {
            List<TeamMember> allTeamMembers = await TeamMember.GetAllEventTeamMembers(Globals.ActiveEvent.eventID);
            await Navigation.PushModalAsync(new ModalEventMembers(allTeamMembers, team));
        }
    }
}
