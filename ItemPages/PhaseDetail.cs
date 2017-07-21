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
    public class PhaseDetails : ContentPage
    {
        Button btnDeliverables;
        Label lblEventName;
        Label lblPhaseName;
        Label lblPhaseOrder;
        Entry entPhaseName;
        Entry entPhaseOrder;
        Button btnSaveClose;
        Button btnSave;
        Button btnDelete;
        ActivityIndicator spinner;

        Phase phase;

        public PhaseDetails(Phase _phase)
        {
            phase = _phase;

            btnDeliverables = new Button { Text = "See Deliverables" };
            btnDeliverables.Clicked += OnbtnDeliverablesClick;
            lblEventName = new Label { Text = phase.eventName };
            lblPhaseName = new Label { Text = "Phase Name" };
            entPhaseName = new Entry { Text = phase.phaseName };
            lblPhaseOrder = new Label { Text = "Phase Order" };
            entPhaseOrder = new Entry { Text = phase.phaseOrder.ToString() };
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
                    btnDeliverables,
                    lblEventName,
                    lblPhaseName,
                    entPhaseName,
                    lblPhaseOrder,
                    entPhaseOrder,
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
            phase.phaseName = entPhaseName.Text;
            phase.phaseOrder = int.Parse(entPhaseOrder.Text);

            if (String.IsNullOrEmpty(phase.phaseID))
            {
                phase = await phase.Create();
                await Navigation.PushModalAsync(new listDeliverables(new List<Deliverable>(), phase));
            }
            else
            {
                phase.Save();
            }
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        void Close(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            Navigation.PopModalAsync();
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        void OnbtnDeleteClick(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            phase.Delete();
            Navigation.PopModalAsync();
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        async void OnbtnDeliverablesClick(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            List<Deliverable> allDeliverables = await Deliverable.GetAllDeliverables(phase.phaseID);
            await Navigation.PushModalAsync(new listDeliverables(allDeliverables, phase));
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }
    }
}
