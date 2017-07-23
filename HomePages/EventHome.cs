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
        ActivityIndicator spinner;

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

            spinner = new ActivityIndicator();
            this.Content = new StackLayout
            {
                Children = {
                    btnEventExecution,
                    btnEventManagement,
                    btnTeamManagement,
                    spinner
                }
            };

        }

        async void OnEventExecutionBtnClick(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            Cache.taskList = await Task.GetAllEventTasks(Globals.ActiveEvent.eventID);
            await Navigation.PushModalAsync(new executionDayView());
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        async void OnEventManagementBtnClick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EventDetails(activeEvent));
        }

        async void onTeamManagementBtnClick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new listTeams(activeEvent));
        }
    }
}