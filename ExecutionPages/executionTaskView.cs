﻿using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
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

            public TaskCell(Task _task)
            {
                this.task = _task;
                this.name = _task.taskName;
                this.assignedToName = _task.assignedToName;
                this.dueDate = _task.dueDateLocal;
                this.dueDateGroup = dueDate.Date.AddHours(dueDate.Hour);
                if (String.IsNullOrEmpty(_task.completedBy))
                {
                    bgColor = Color.White;
                }
                else
                {
                    bgColor = Color.Green;
                }

            }
        };

        class TaskCellGroup : List<TaskCell>
        {
            public DateTime dueDateGroup { get; set; }
        }

        public executionTaskView(List<Task> taskList)
        {
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

            // Create the ListView.
            ListView listView = new ListView
            {
                // Source of data items.
                
                ItemsSource = groupedWithTitleProperty,
                HasUnevenRows = true,
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding("dueDateGroup"),

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "name");
                    //nameLabel.SetBinding(Label.HorizontalOptionsProperty, "Hor");

                    Label dueLabel = new Label();
                    dueLabel.SetBinding(Label.TextProperty, "dueDate", BindingMode.Default, null, stringFormat: "Due: {0:}");

                    //Label dueGroupLabel = new Label();
                    //dueGroupLabel.SetBinding(Label.TextProperty, "dueDateGroup", BindingMode.Default, null, stringFormat: "Due Group: {0:}");

                    Label assignedToLabel = new Label();
                    assignedToLabel.SetBinding(Label.TextProperty, "assignedToName", BindingMode.Default, null, stringFormat: "Assigned To: {0:}");

                    StackLayout myView = new StackLayout
                    {

                        Padding = new Thickness(0, 5),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Spacing = 0,
                        Children =
                        {
                            nameLabel,
                            //dueGroupLabel,
                            dueLabel,
                            assignedToLabel
                        }

                                        
                    };

                    
                    myView.SetBinding(BackgroundColorProperty, "bgColor");
                    
                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = myView  
                                                                                       
                    };
                })
            };

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

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listView
                }
            };

        }
    }
}
