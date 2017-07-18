using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using FITEvents.Classes;
using FITEvents.ListPages;
using FITEvents.ModalPages;
using Xamarin.Forms;

namespace FITEvents.ItemPages
{
	public class DeliverableDetails : ContentPage
	{
        Button btnTasks;
        Label lblphaseName;
        Label lbldeliverableName;
        Label lblvendorName;
        Label lblteam;
        Label lblpriority;
        Label lblnotes;
        Entry entdeliverableName;
        Entry entvendorName;
        Button btnteam;
        Entry entpriority;
        Entry entnotes;
        Button btnSaveClose;
        Button btnSave;
        Button btnDelete;

        Deliverable deliverable;
        
        public DeliverableDetails (Deliverable passedDeliverable)
		{
            deliverable = passedDeliverable;

            btnTasks = new Button { Text = "View Tasks" };
            btnTasks.Clicked += OnbtnTasksClick;
            lblphaseName = new Label { Text = deliverable.phaseName };
            lbldeliverableName = new Label { Text = "Deliverable Name" };
            entdeliverableName = new Entry { Text = deliverable.deliverableName };
            lblteam = new Label { Text = "Team" };
            btnteam = new Button { Text = deliverable.teamName };
            btnteam.Clicked += OnTeamBtnClicked;
            lblpriority = new Label { Text = "Priority" };
            entpriority = new Entry { Text = deliverable.priority.ToString()};
            lblvendorName = new Label { Text = "Vendor" };
            entvendorName = new Entry { Text = deliverable.vendorName };
            lblnotes = new Label { Text = "Notes" };
            entnotes = new Entry { Text = deliverable.notes };
            btnSaveClose = new Button { Text = "Save and Close" };
            btnSaveClose.Clicked += Save;
            btnSaveClose.Clicked += Close;
            btnSave = new Button { Text = "Save" };
            btnSave.Clicked += Save;
            btnDelete = new Button { Text = "Delete" };
            btnDelete.Clicked += OnbtnDeleteClick;

            StackLayout stack = new StackLayout {
                Children = {
                    btnTasks,
					lblphaseName,
                    lbldeliverableName,
                    entdeliverableName,
                    lblteam,
                    btnteam,
                    lblpriority,
                    entpriority,
                    lblvendorName,
                    entvendorName,
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

        async void Save(object sender, EventArgs e)
        {
            deliverable.deliverableName = entdeliverableName.Text;
            deliverable.vendorID = entvendorName.Text;
            deliverable.priority = int.Parse(entpriority.Text);
            deliverable.notes = entnotes.Text;

            if (String.IsNullOrEmpty(deliverable.deliverableID))
            {
                deliverable = await deliverable.Create();
                await Navigation.PushModalAsync(new listTasks(new List<Task>(), deliverable));
            }

            else
            {
                deliverable.Save();
            }
        }

        void Close(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void OnbtnDeleteClick(object sender, EventArgs e)
        {
            deliverable.Delete();
        }

        void OnbtnTasksClick(object sender, EventArgs e)
        {
            List<Task> allTasks = Task.GetAllTasks(deliverable.deliverableID);
            Navigation.PushModalAsync(new listTasks(allTasks, deliverable));
        }

        void OnTeamBtnClicked(object sender = null, EventArgs e = null)
        {
            List<Team> allTeams = Team.GetAllTeams(Globals.ActiveEvent.eventID);
            Navigation.PushModalAsync(new ModalTeam(this, allTeams));
        }

        public void updateTeam(Team team)
        {
            deliverable.team = team.teamID;
            deliverable.teamName = team.teamName;
            btnteam.Text = deliverable.teamName;
        }
    }
}
