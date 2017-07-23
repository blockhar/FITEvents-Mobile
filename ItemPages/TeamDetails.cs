using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.Classes;
using FITEvents.ListPages;
using Android.Util;

using Xamarin.Forms;

namespace FITEvents.ItemPages
{
    public class TeamDetails : ContentPage
    {
        Button btnMembers;
        Label lblEventName;
        Label lblTeamName;
        Entry entTeamName;
        Button btnSaveClose;
        Button btnSave;
        Button btnDelete;

        Team team;

        public TeamDetails(Team _team)
        {
            team = _team;

            btnMembers = new Button { Text = "See Team Members" };
            btnMembers.Clicked += OnbtnMembersClick;
            lblEventName = new Label { Text = team.eventName };
            lblTeamName = new Label { Text = "Team Name" };
            entTeamName = new Entry { Text = team.teamName };
            btnSaveClose = new Button { Text = "Save and Close" };
            btnSaveClose.Clicked += Save;
            btnSaveClose.Clicked += Close;
            btnSave = new Button { Text = "Save" };
            btnSave.Clicked += Save;
            btnDelete = new Button { Text = "Delete" };
            btnDelete.Clicked += OnbtnDeleteClick;

            StackLayout stack = new StackLayout
            {
                Children = {
                    btnMembers,
                    lblEventName,
                    lblTeamName,
                    entTeamName,
                    btnSaveClose,
                    btnSave,
                    btnDelete
                }
            };

            this.Content = new ScrollView
            {
                Content = stack
            };
        }

        async void Save(object sender, EventArgs e)
        {
            team.teamName = entTeamName.Text;

            Log.Info("FITEVENTS", "Enter Save team stream");

            if (String.IsNullOrEmpty(team.teamID))
            {
                Log.Info("FITEVENTS", "Try Create team");
                team = await team.Create();
                Log.Info("FITEVENTS", "Team created.");
                await Navigation.PushModalAsync(new listTeamMembers(team));
            }
            else
            {
                team.Save();
            }
        }

        void Close(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void OnbtnDeleteClick(object sender, EventArgs e)
        {

        }

        async void OnbtnMembersClick(object sender, EventArgs e)
        {         
            await Navigation.PushModalAsync(new listTeamMembers(team));
        }
    }
}
