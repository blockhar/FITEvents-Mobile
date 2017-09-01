using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;

using Xamarin.Forms;

namespace FITEvents.ListPages
{
    public class listDeliverables : ContentPage
    {
        List<Deliverable> allDeliverables;
        ListView listView;
        Button btnNewDeliverable;
        Phase phase;
        ActivityIndicator spinner;

        public listDeliverables(Phase _phase)
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

            phase = _phase;
            btnNewDeliverable = new Button { Text = "Create New Deliverable" };
            btnNewDeliverable.Clicked += OnbtnNewDeliverableClick;
            spinner = new ActivityIndicator();

            listView = new ListView
            {

                ItemsSource = allDeliverables,
                HasUnevenRows = true,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "deliverableName");

                    Label teamLabel = new Label();
                    teamLabel.SetBinding(Label.TextProperty, "teamName", BindingMode.Default, null, stringFormat: "Team: {0:}");

                    Label vendorLabel = new Label();
                    vendorLabel.SetBinding(Label.TextProperty, "vendorName", BindingMode.Default, null, stringFormat: "Vendor: {0:}");

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
            listView.SeparatorColor = Color.White;

            Content = new StackLayout
            {
                Children =
                {
                    btnNewDeliverable,
                    listView,
                    spinner
                }
            };

        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            spinner.IsRunning = true;
            spinner.IsVisible = true;

            Deliverable selectedDeliverable = (Deliverable)e.SelectedItem;
            Navigation.PushModalAsync(new DeliverableDetails(selectedDeliverable));
            ((ListView)sender).SelectedItem = null;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

        }

        void OnbtnNewDeliverableClick(object sender, EventArgs e)
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            Deliverable newDeliverable = new Deliverable();
            newDeliverable.phaseID = phase.phaseID;
            newDeliverable.phaseName = phase.phaseName;
            Navigation.PushModalAsync(new DeliverableDetails(newDeliverable));
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }

        async override protected void OnAppearing()
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            allDeliverables = await Deliverable.GetAllDeliverables(phase.phaseID);
            listView.ItemsSource = allDeliverables;
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }
    }
}
