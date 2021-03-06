﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        Button btnClear;
        TaskDetails taskPage;
        String updateField;
        ActivityIndicator spinner;

        public ModalTeamMember(TaskDetails _taskPage, List<TeamMember> allTeamMembers, String _updateField)
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

            taskPage = _taskPage;
            updateField = _updateField;
            
            btnNewTeamMember = new Button { Text = "Create New TeamMember" };
            btnNewTeamMember.Clicked += OnbtnNewTeamMemberClick;
            btnClear = new Button { Text = "Clear" };
            btnClear.Clicked += OnbtnClearClick;
            spinner = new ActivityIndicator();

            listView = new ListView
            {

                ItemsSource = allTeamMembers,
                HasUnevenRows = true,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "teamMemberName");

                    Label emailLable = new Label();
                    emailLable.SetBinding(Label.TextProperty, "teamMemberEmail");

                    Label teamLabel = new Label();
                    teamLabel.SetBinding(Label.TextProperty, "teamName");

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
            listView.SeparatorColor = Color.White;

            Content = new StackLayout
            {
                Children =
                {
                    btnNewTeamMember,
                    btnClear,
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
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }
        void OnbtnClearClick(object sender, EventArgs e)
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            if (updateField == "assignedTo")
            {
                taskPage.clearAssignedTo();
            }
            else if (updateField == "completedBy")
            {
                taskPage.clearCompletedBy();
            }

            Navigation.PopModalAsync();
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }
        


        void OnbtnNewTeamMemberClick(object sender, EventArgs e)
        {
            TeamMember newTeamMember = new TeamMember();
            //Navigation.PushModalAsync(new TeamMemberDetails(newTeamMember));
        }
    }
}
