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
                    btnDelete
                }
            };

            this.Content = new ScrollView
            {
                Content = stack
            };
        }

        void Save(object sender, EventArgs e)
        {
            phase.phaseName = entPhaseName.Text;
            phase.phaseOrder = int.Parse(entPhaseOrder.Text);

            if (String.IsNullOrEmpty(phase.phaseID))
            {
                phase = phase.Create();
                Navigation.PushModalAsync(new listDeliverables(new List<Deliverable>(), phase));
            }
            else
            {
                phase.Save();
            }
        }

        void Close(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void OnbtnDeleteClick(object sender, EventArgs e)
        {

        }

        async void OnbtnDeliverablesClick(object sender, EventArgs e)
        {
            List<Deliverable> allDeliverables = await Deliverable.GetAllDeliverables(phase.phaseID);
            Navigation.PushModalAsync(new listDeliverables(allDeliverables, phase));
        }
    }
}
