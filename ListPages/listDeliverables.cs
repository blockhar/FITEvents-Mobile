using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;

using Xamarin.Forms;

namespace FITEvents.ListPages
{
    public class listDeliverables : ContentPage
    {

        ListView listView;
        Button btnNewDeliverable;
        Phase phase;

        public listDeliverables(List<Deliverable> allDeliverables, Phase _phase)
        {
            phase = _phase;
            btnNewDeliverable = new Button { Text = "Create New Deliverable" };
            btnNewDeliverable.Clicked += OnbtnNewDeliverableClick;

            listView = new ListView
            {

                // Source of data items.
                ItemsSource = allDeliverables,
                HasUnevenRows = true,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "deliverableName");

                    Label teamLabel = new Label();
                    teamLabel.SetBinding(Label.TextProperty, "teamName", BindingMode.Default, null, stringFormat: "Team: {0:}");

                    Label vendorLabel = new Label();
                    vendorLabel.SetBinding(Label.TextProperty, "vendorName", BindingMode.Default, null, stringFormat: "Vendor: {0:}");

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Children =
                            {
                                nameLabel,
                                teamLabel,
                                vendorLabel
                            }
                        }
                    };
                })
            };

            listView.ItemSelected += OnSelection;

            Content = new StackLayout
            {
                Children =
                {
                    btnNewDeliverable,
                    listView
                }
            };
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            Deliverable selectedDeliverable = (Deliverable)e.SelectedItem;
            Navigation.PushModalAsync(new DeliverableDetails(selectedDeliverable));
            ((ListView)sender).SelectedItem = null;

        }

        void OnbtnNewDeliverableClick(object sender, EventArgs e)
        {
            Deliverable newDeliverable = new Deliverable();
            newDeliverable.phaseID = phase.phaseID;
            newDeliverable.phaseName = phase.phaseName;
            Navigation.PushModalAsync(new DeliverableDetails(newDeliverable));
        }
    }
}
