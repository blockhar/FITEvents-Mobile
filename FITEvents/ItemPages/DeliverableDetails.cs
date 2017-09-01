using System;
using System.Collections.Generic;
using System.Linq;
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
        Picker pckpriority;
        Entry entnotes;
        Button btnSaveClose;
        Button btnSave;
        Button btnDelete;
        ActivityIndicator spinner;

        Deliverable deliverable;
        
        public DeliverableDetails (Deliverable passedDeliverable)
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

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
            pckpriority = new Picker {Title = "Priority"};
            pckpriority.SelectedIndexChanged += OnpckPriorityChange;
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
            spinner = new ActivityIndicator();

            //Add priority options to picker
            pckpriority.Items.Add("High");
            pckpriority.Items.Add("Low");

            //Set existing value for priority picker
            if (!String.IsNullOrEmpty(deliverable.priority))
            {
                for (int i = 0; i < pckpriority.Items.Count; i++)
                {
                    if (pckpriority.Items[i] == deliverable.priority)
                    {
                        pckpriority.SelectedIndex = i;
                    }
                }
            }

            StackLayout stack = new StackLayout {
                Children = {
                    btnTasks,
					lblphaseName,
                    lbldeliverableName,
                    entdeliverableName,
                    lblteam,
                    btnteam,
                    lblpriority,
                    pckpriority,
                    lblvendorName,
                    entvendorName,
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

        async void Save(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            deliverable.deliverableName = entdeliverableName.Text;
            deliverable.vendorID = entvendorName.Text;
            deliverable.notes = entnotes.Text;

            if (String.IsNullOrEmpty(deliverable.deliverableID))
            {
                deliverable = await deliverable.Create();
                await Navigation.PushAsync(new listTasks(deliverable));
            }

            else
            {
                deliverable.Save();
            }
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        async void Close(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void OnbtnDeleteClick(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            await deliverable.Delete();
            await Navigation.PopModalAsync();
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        async void OnbtnTasksClick(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            
            await Navigation.PushAsync(new listTasks(deliverable));
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        async void OnTeamBtnClicked(object sender = null, EventArgs e = null)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            List<Team> allTeams = await Team.GetAllTeams(Globals.ActiveEvent.eventID);
            await Navigation.PushAsync(new ModalTeam(this, allTeams));
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        public void updateTeam(Team team)
        {
            deliverable.team = team.teamID;
            deliverable.teamName = team.teamName;
            btnteam.Text = deliverable.teamName;
        }

        void OnpckPriorityChange(object sender, EventArgs e)
        {
            deliverable.priority = (string)pckpriority.SelectedItem;
        }
        
    }
}
