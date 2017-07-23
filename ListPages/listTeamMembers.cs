using System;
using System.Collections.Generic;
using System.Linq;
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
        ActivityIndicator spinner;
        List<TeamMember> allTeamMembers;

        public listTeamMembers(Team _team)
        {
            team = _team;
            btnNewTeamMember = new Button { Text = "Add TeamMember" };
            btnNewTeamMember.Clicked += OnbtnNewTeamMemberClick;
            spinner = new ActivityIndicator();

            listView = new ListView
            {

                ItemsSource = allTeamMembers,
                HasUnevenRows = true,


                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "teamMemberName");

                    Label teamLabel = new Label();
                    teamLabel.SetBinding(Label.TextProperty, "teamName");

                    Label emailLabel = new Label();
                    emailLabel.SetBinding(Label.TextProperty, "teamMemberEmail");

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
            TeamMember selectedTeamMember = (TeamMember)e.SelectedItem;
            
            ((ListView)sender).SelectedItem = null; 
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }

        private async void OnbtnNewTeamMemberClick(object sender, EventArgs e)
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            List<TeamMember> allTeamMembers = await TeamMember.GetAllEventTeamMembers(Globals.ActiveEvent.eventID);
            await Navigation.PushModalAsync(new ModalEventMembers(allTeamMembers, team));
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }

        async override protected void OnAppearing()
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            allTeamMembers = await TeamMember.GetAllTeamMembers(team.teamID);
            listView.ItemsSource = allTeamMembers;
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }
    }
}
