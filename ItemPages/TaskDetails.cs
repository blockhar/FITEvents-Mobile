using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
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

        Task task;

        public TaskDetails (Task passedtask)
		{
            task = passedtask;

            lbltaskName = new Label { Text = "Task Name" };
            enttaskName = new Entry { Text = task.taskName };
            lbldueDate = new Label { Text = "Due Date"};
            entdueDate = new DatePicker { Date = task.dueDateLocal.Date };
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
                    btnDelete                    
                }
			};

            this.Content = new ScrollView
            {
                Content = stack
            };

        }

        void OnCompletedByClicked(object sender = null, EventArgs e = null)
        {
            List<TeamMember> allTeamMembers = TeamMember.GetAllTeamMembers(Globals.ActiveEvent.eventID);
            //List<TeamMember> allTeamMembers = TeamMember.GetAllTeamMembers("eventID123");
            Navigation.PushModalAsync(new ModalTeamMember(this, allTeamMembers, "completedBy"));
        }

        void OnAssignedToClicked(object sender = null, EventArgs e = null)
        {
            List<TeamMember> allTeamMembers = TeamMember.GetAllTeamMembers(Globals.ActiveEvent.eventID);
            Navigation.PushModalAsync(new ModalTeamMember(this, allTeamMembers, "assignedTo"));
        }

        void Save(object sender, EventArgs e)
        {
            task.taskName = enttaskName.Text;
            task.dueDate = DateTime.Parse(entdueDate.Date.ToShortDateString() + " " + entdueTime.Time.ToString());
            task.notes = entnotes.Text;
            if (String.IsNullOrEmpty(task.taskID))
            {
                task = task.Create();
            }
            else
            {
                task.Save();
            }
        }

        void OnbtnDeleteClick(object sender, EventArgs e)
        {

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
    }
}
