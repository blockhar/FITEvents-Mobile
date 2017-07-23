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
    public class listInvitations : ContentPage
    {

        ListView listView;
        ActivityIndicator spinner;

        public listInvitations(List<TeamInvitation> allInvites)
        {
            spinner = new ActivityIndicator();
            listView = new ListView
            {

                // Source of data items.
                ItemsSource = allInvites,
                HasUnevenRows = true,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "teamName");

                    Label eventLabel = new Label();
                    eventLabel.SetBinding(Label.TextProperty, "eventName");

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
                                eventLabel
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
            TeamInvitation selectedInvite = (TeamInvitation)e.SelectedItem;
            Navigation.PushModalAsync(new InvitationDetails(selectedInvite));
            ((ListView)sender).SelectedItem = null;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

        }
    }
}
