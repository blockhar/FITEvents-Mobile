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

            StackLayout stack;

            if (Globals.loggedInUser.userEmail == teaminvitation.inviteEmail)
            {
                stack = new StackLayout
                {
                    Children = {
                    lblTeamName,
                    btnAccept,
                    btnDecline
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
                    btnSend
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
            teaminvitation.inviteEmail = entInviteEmail.Text;
            await teaminvitation.Create();
            await DisplayAlert("Confirmation", "Invitation has been sent!", "OK");
            await Navigation.PopModalAsync();
        }

        void Accept(object sender, EventArgs e)
        {
            teaminvitation.Accept();
            Navigation.PopModalAsync();
        }

        void Decline(object sender, EventArgs e)
        {
            teaminvitation.Decline();
            Navigation.PopModalAsync();
        }
    }
}
