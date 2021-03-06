﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;
using Xamarin.Forms;
using Android.Util;

namespace FITEvents.ExecutionPages
{
    public class executionTaskView : ContentPage
    {
        class TaskCell
        {
            public Task task { get; set; }
            public string name { get; set; }
            public DateTime dueDate { get; set; }
            public DateTime dueDateGroup { get; set; }
            public string assignedToName { get; set; }
            public Color bgColor { get; set; }
            public string status { get; set; }

            public TaskCell(Task _task)
            {
                this.task = _task;
                this.name = _task.taskName;
                this.assignedToName = _task.assignedToName;
                this.dueDate = _task.dueDateLocal;
                this.dueDateGroup = dueDate.Date.AddHours(dueDate.Hour);
                if (String.IsNullOrEmpty(_task.completedBy))
                {
                    bgColor = Color.Transparent;
                    status = "Not Completed";
                }
                else
                {
                    bgColor = Color.Green;
                    status = "Completed";
                }

            }
        };

        class TaskCellGroup : List<TaskCell>
        {
            public DateTime dueDateGroup { get; set; }
        }

        public executionTaskView(List<Task> taskList)
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

            Label header = new Label
            {
                Text = "Event Execution - " + Globals.ActiveEvent.eventName,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Get items from cache and create taskCell for each
            List<TaskCell> taskCells = new List<TaskCell>();

            foreach (Task task in taskList)
            {
                Log.Info("FITEVENTS", "Task AssignedToName is: " + task.assignedToName);
                TaskCell taskCell = new TaskCell(task);
                Log.Info("FITEVENTS", "TaskCell AssignedToName is: " + taskCell.assignedToName);
                taskCells.Add(taskCell);
            }

            List<TaskCell> SortedTaskCells = taskCells.OrderBy(t => t.dueDate).ToList();

            List<List<TaskCell>> groupedTaskCells = SortedTaskCells.GroupBy(t => t.dueDateGroup).Select(grp => grp.ToList()).ToList();

            List<TaskCellGroup> groupedWithTitleProperty = new List<TaskCellGroup>();

            foreach (List<TaskCell> list in groupedTaskCells)
            {
                TaskCellGroup group = new TaskCellGroup();
                group.dueDateGroup = list.First().dueDateGroup;
                group.AddRange(list);
                groupedWithTitleProperty.Add(group);
            }


            ListView listView = new ListView
            {

                ItemsSource = groupedWithTitleProperty,
                HasUnevenRows = true,
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding("dueDateGroup", BindingMode.Default, null, stringFormat: "{0:t}"),

                GroupHeaderTemplate = new DataTemplate(() =>
                {
                    Label dueDateGroupLabel = new Label();
                    dueDateGroupLabel.SetBinding(Label.TextProperty, "dueDateGroup", BindingMode.Default, null, stringFormat: "{0:t}");

                    StackLayout myGroupView = new StackLayout
                    {
                        Padding = new Thickness(0, 5),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Spacing = 0,
                        Children =
                        {
                            dueDateGroupLabel
                        }
                    };

                    return new ViewCell
                    {
                        View = myGroupView
                    };
                }),

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "name");
                    
                    Label dueLabel = new Label();
                    dueLabel.SetBinding(Label.TextProperty, "dueDate", BindingMode.Default, null, stringFormat: "Due: {0:t}");
                 
                    Label assignedToLabel = new Label();
                    assignedToLabel.SetBinding(Label.TextProperty, "assignedToName", BindingMode.Default, null, stringFormat: "Assigned To: {0:}");

                    Label statusLabel = new Label();
                    statusLabel.SetBinding(Label.TextProperty, "status", BindingMode.Default, null, stringFormat: "Status: {0:}");

                    StackLayout myView = new StackLayout
                    {

                        Padding = new Thickness(0, 5),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Spacing = 0,
                        Children =
                        {
                            nameLabel,
                            dueLabel,
                            assignedToLabel,
                            statusLabel
                        }

                                        
                    };

                    
                    myView.SetBinding(BackgroundColorProperty, "bgColor");
                    
                    return new ViewCell
                    {
                        View = myView  
                                                                                       
                    };
                                        
                })
            };

            listView.ItemSelected += OnSelection;

            // Accomodate iPhone status bar.
            int top;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    top = 20;
                    break;
                default:
                    top = 0;
                    break;
            }
            this.Padding = new Thickness(10, top, 10, 5);

            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listView
                }
            };

        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            //spinner.IsVisible = true;
            //spinner.IsRunning = true;
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            TaskCell cell = (TaskCell)e.SelectedItem;
            Navigation.PushAsync(new TaskDetails(cell.task, ""));
            //spinner.IsVisible = false;
            //spinner.IsRunning = false;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
