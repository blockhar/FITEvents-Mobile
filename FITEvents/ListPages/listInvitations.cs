using System;
using System.Collections.Generic;
using System.Linq;
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
        List<TeamInvitation> allInvites;

        public listInvitations()
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

            spinner = new ActivityIndicator();
            listView = new ListView
            {

                ItemsSource = allInvites,
                HasUnevenRows = true,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "teamName");

                    Label eventLabel = new Label();
                    eventLabel.SetBinding(Label.TextProperty, "eventName");

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
            listView.SeparatorColor = Color.White;

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

        async override protected void OnAppearing()
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            allInvites = await TeamInvitation.GetAllEmailInvites();
            allInvites = allInvites.Where(i => i.status == "Pending").ToList();
            listView.ItemsSource = allInvites;
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }
    }
}
