using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using FITEvents.Classes;
using FITEvents.ListPages;

using Xamarin.Forms;

namespace FITEvents.ItemPages
{
    public class InvitationDetails : ContentPage
    {
        Label lblTeamName;
        Label lblInviteEmail;
        Entry entInviteEmail;
        Button btnSend;
        Button btnAccept;
        Button btnDecline;
        ActivityIndicator spinner;

        TeamInvitation teaminvitation;

        public InvitationDetails(TeamInvitation _invitation)
        {
            teaminvitation = _invitation;

            lblTeamName = new Label { Text = teaminvitation.teamName };
            lblInviteEmail = new Label { Text = "Invite by email" };
            entInviteEmail = new Entry { Placeholder = "email@example.com" };
            btnSend = new Button { Text = "Send Invitation" };
            btnSend.Clicked += Send;
            btnAccept = new Button { Text = "Accept" };
            btnAccept.Clicked += Accept;
            btnDecline = new Button { Text = "Decline" };
            btnDecline.Clicked += Decline;
            spinner = new ActivityIndicator();

            StackLayout stack;

            if (Globals.loggedInUser.userEmail == teaminvitation.inviteEmail)
            {
                stack = new StackLayout
                {
                    Children = {
                    lblTeamName,
                    btnAccept,
                    btnDecline,
                    spinner
                }
                };
            }
            else
            {
                stack = new StackLayout
                {
                    Children = {
                    lblTeamName,
                    lblInviteEmail,
                    entInviteEmail,
                    btnSend,
                    spinner
                }
                };
            }
            

            this.Content = new ScrollView
            {
                Content = stack
            };
        }

        async void Send(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            teaminvitation.inviteEmail = entInviteEmail.Text;
            await teaminvitation.Create();
            await DisplayAlert("Confirmation", "Invitation has been sent!", "OK");
            await Navigation.PopModalAsync();
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        void Accept(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            teaminvitation.Accept();
            Navigation.PopModalAsync();
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        void Decline(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            teaminvitation.Decline();
            Navigation.PopModalAsync();
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }
    }
}
