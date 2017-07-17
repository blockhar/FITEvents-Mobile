using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;

using Xamarin.Forms;

namespace FITEvents.ModalPages
{
    public class ModalTeamMember : ContentPage
    {

        ListView listView;
        Button btnNewTeamMember;
        TaskDetails taskPage;
        String updateField;

        public ModalTeamMember(TaskDetails _taskPage, List<TeamMember> allTeamMembers, String _updateField)
        {
            taskPage = _taskPage;
            updateField = _updateField;
            //taskPage.OnCompletedByClicked();

            btnNewTeamMember = new Button { Text = "Create New TeamMember" };
            btnNewTeamMember.Clicked += OnbtnNewTeamMemberClick;

            listView = new ListView
            {

                // Source of data items.
                ItemsSource = allTeamMembers,
                HasUnevenRows = true,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "teamMemberName");

                    Label emailLable = new Label();
                    emailLable.SetBinding(Label.TextProperty, "teamMemberEmail");

                    Label teamLabel = new Label();
                    teamLabel.SetBinding(Label.TextProperty, "teamName");

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
                                emailLable,
                                teamLabel
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
                    btnNewTeamMember,
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

            TeamMember selectedTeamMember = (TeamMember)e.SelectedItem;
            if (updateField == "assignedTo")
            {
                taskPage.updateAssignedTo(selectedTeamMember);
            }
            else if (updateField == "completedBy")
            {
                taskPage.updateCompletedBy(selectedTeamMember);
            }
            
            Navigation.PopModalAsync();

        }

        void OnbtnNewTeamMemberClick(object sender, EventArgs e)
        {
            TeamMember newTeamMember = new TeamMember();
            //Navigation.PushModalAsync(new TeamMemberDetails(newTeamMember));
        }
    }
}
