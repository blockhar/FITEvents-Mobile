using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;
using FITEvents.ListPages;
using FITEvents.ExecutionPages;
using Xamarin.Forms;

namespace FITEvents.HomePages
{
    public class EventHome : ContentPage
    {
        Button btnEventExecution;
        Button btnEventManagement;
        Button btnTeamManagement;
        Event activeEvent;

        public EventHome(Event _activeEvent)
        {
            activeEvent = _activeEvent;

            btnEventExecution = new Button
            {
                Text = "Event Execution"
            };
            btnEventExecution.Clicked += OnEventExecutionBtnClick;

            btnEventManagement = new Button
            {
                Text = "Event Management"
            };
            btnEventManagement.Clicked += OnEventManagementBtnClick;

            btnTeamManagement = new Button { Text = "Team Management" };
            btnTeamManagement.Clicked += onTeamManagementBtnClick;

            this.Content = new StackLayout
            {
                Children = {
                    btnEventExecution,
                    btnEventManagement,
                    btnTeamManagement
                }
            };

        }

        void OnEventExecutionBtnClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new executionDayView());
        }

        void OnEventManagementBtnClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EventDetails(activeEvent));
        }

        void onTeamManagementBtnClick(object sender, EventArgs e)
        {
            List<Team> allTeams = Team.GetAllTeams(activeEvent.eventID);
            Navigation.PushModalAsync(new listTeams(allTeams, activeEvent));
        }
    }
}