using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;
using FITEvents.ListPages;
using FITEvents.ExecutionPages;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace FITEvents.HomePages
{
    public class Home : ContentPage
    {

        Button btnProfile;
        Button btnEvents;
        Button btnInvitations;
        ActivityIndicator spinner;

        public Home()
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

            btnProfile = new Button
            {
                Text = "My Profile"
            };
            btnProfile.Clicked += OnProfileBtnClick;

            btnEvents = new Button
            {
                Text = "Events"
            };
            btnEvents.Clicked += OnEventsBtnClick;

            btnInvitations = new Button
            {
                Text = "Invitations"
            };
            btnInvitations.Clicked += OnInvitationsBtnClick;

            spinner = new ActivityIndicator();

            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    btnProfile,
                    btnEvents,
                    btnInvitations,
                    spinner
                }
            };

            this.ToolbarItems.Add(new ToolbarItem("", "menu.png", OnMenuButton, ToolbarItemOrder.Primary));
            
        }

        void OnMenuButton()
        {
            DisplayAlert("Test", "Thanks for pressing the menu button", "Ok");
        }

        void OnProfileBtnClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserDetails(Globals.loggedInUser));
        }

        async void OnEventsBtnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new listEvents());
        }

        
       async void OnInvitationsBtnClick(object sender, EventArgs e)
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            await Navigation.PushAsync(new listInvitations());
            spinner.IsRunning = false;
            spinner.IsVisible = false;

        }
    }
}