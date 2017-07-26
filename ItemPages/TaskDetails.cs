using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.Classes;
using FITEvents.ModalPages;
using Xamarin.Forms;
using Android.Util;

namespace FITEvents.ItemPages
{
	public class TaskDetails : ContentPage
	{
        Label lbltaskName;
        Label lbldueDate;
        Label lblassignedTo;
        Label lblcompletedBy;
        Label lblnotes;
        Entry enttaskName;
        DatePicker entdueDate;
        TimePicker entdueTime;
        Button btnassignedTo;
        Button btncompletedBy;
        Entry entnotes;
        Button btnSave;
        Button btnSaveClose;
        Button btnDelete;
        ActivityIndicator spinner;

        Task task;
        string teamID;

        public TaskDetails (Task passedtask, string _teamID)
		{
            task = passedtask;
            teamID = _teamID;

            lbltaskName = new Label { Text = "Task Name" };
            enttaskName = new Entry { Text = task.taskName };
            lbldueDate = new Label { Text = "Due Date"};
            //Check if task has a date. If not, default to date of parent event.
            entdueDate = new DatePicker { Date = (task.dueDate == DateTime.MinValue ? Globals.ActiveEvent.eventDate : task.dueDate) };
            entdueTime = new TimePicker { Time = task.dueDateLocal.TimeOfDay };
            lblassignedTo = new Label { Text = "Assigned To"}; 
            btnassignedTo = new Button { Text = task.assignedToName};
            btnassignedTo.Clicked += OnAssignedToClicked;
            lblcompletedBy = new Label { Text = "Completed By"};            
            btncompletedBy = new Button { Text = task.completedByName};
            btncompletedBy.Clicked += OnCompletedByClicked;
            lblnotes = new Label { Text = "Notes"};
            entnotes = new Entry { Text = task.notes};
            btnSaveClose = new Button { Text = "Save and Close" };
            btnSaveClose.Clicked += Save;
            btnSaveClose.Clicked += Close;
            btnSave = new Button { Text = "Save" };
            btnSave.Clicked += Save;
            btnDelete = new Button { Text = "Delete" };
            btnDelete.Clicked += OnbtnDeleteClick;
            spinner = new ActivityIndicator();

            StackLayout stack = new StackLayout
            {
                Children = {
                    lbltaskName,
                    enttaskName,
                    lbldueDate,
                    entdueDate,
                    entdueTime,
                    lblassignedTo,
                    btnassignedTo,
                    lblcompletedBy,
                    btncompletedBy,
                    lblnotes,
                    entnotes,
                    btnSaveClose,
                    btnSave,
                    btnDelete,
                    spinner                
                }
			};

            this.Content = new ScrollView
            {
                Content = stack
            };

        }

        async void OnCompletedByClicked(object sender = null, EventArgs e = null)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            List<TeamMember> allTeamMembers = await TeamMember.GetAllTeamMembers(teamID);
            await Navigation.PushModalAsync(new ModalTeamMember(this, allTeamMembers, "completedBy"));
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        async void OnAssignedToClicked(object sender = null, EventArgs e = null)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            List<TeamMember> allTeamMembers = await TeamMember.GetAllTeamMembers(teamID);
            await Navigation.PushModalAsync(new ModalTeamMember(this, allTeamMembers, "assignedTo"));
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        async void Save(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            task.taskName = enttaskName.Text;
            task.dueDate = DateTime.Parse(entdueDate.Date.ToShortDateString() + " " + entdueTime.Time.ToString());
            task.notes = entnotes.Text;
            if (String.IsNullOrEmpty(task.taskID))
            {
                task = await task.Create();
            }
            else
            {
                task.Save();
            }
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        void OnbtnDeleteClick(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            task.Delete();
            spinner.IsVisible = false;
            spinner.IsRunning = false;

        }

        void Close(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void updateAssignedTo(TeamMember assignedTo)
        {
            task.assignedTo = assignedTo.teamMemberID;
            task.assignedToName = assignedTo.teamMemberName;
            btnassignedTo.Text = task.assignedToName;
        }

        public void updateCompletedBy(TeamMember completedBy)
        {
            task.completedBy = completedBy.teamMemberID;
            task.completedByName = completedBy.teamMemberName;
            btncompletedBy.Text = task.completedByName;
        }

        public void clearAssignedTo()
        {
            task.assignedTo = null;
            task.assignedToName = null;
            btnassignedTo.Text = "";
        }

        public void clearCompletedBy()
        {
            task.completedBy = null;
            task.completedByName = null;
            btncompletedBy.Text = "";
        }
    }
}
