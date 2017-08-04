using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.Classes;
using FITEvents.ItemPages;
using Xamarin.Forms;
using Android.Util;

namespace FITEvents.ExecutionPages
{
    public class executionDayView : ContentPage
    {
        ActivityIndicator spinner;

        class TaskCell
        {
            public Task task { get; set; }
            public string name { get; set; }
            public DateTime dueDate { get; set; }
            public DateTime dueDateGroup { get; set; }
            public string assignedToName { get; set; }

            public TaskCell(Task _task)
            {
                this.BackgroundImage = FITEventStyles.GlobalBGImageName;

                this.task = _task;
                this.name = _task.taskName;
                this.assignedToName = _task.assignedToName;
                this.dueDate = _task.dueDateLocal;
                this.dueDateGroup = dueDate.Date;

            }
        };

        class TaskCellGroup : List<TaskCell>
        {
            public DateTime dueDateGroup { get; set; }
        }

        public executionDayView()
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

            foreach (Task task in Cache.taskList)
            {
                TaskCell taskCell = new TaskCell(task);
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

            spinner = new ActivityIndicator();
            
            ListView listView = new ListView
            {
                
                ItemsSource = groupedWithTitleProperty,
                HasUnevenRows = true,

                ItemTemplate = new DataTemplate(() =>
                {
                    
                    Label dueLabel = new Label();
                    dueLabel.SetBinding(Label.TextProperty, "dueDateGroup", BindingMode.Default, null, stringFormat: "{0:d}");

                    StackLayout myView = new StackLayout
                    {

                        Padding = new Thickness(0, 5),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Spacing = 0,
                        Children =
                        {
                            dueLabel,
                            spinner
                        }


                    };


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
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            TaskCellGroup group = (TaskCellGroup)e.SelectedItem;
            List<Task> taskList = new List<Task>();
            foreach (TaskCell taskCell in group)
            {
                taskList.Add(taskCell.task);
            }
            Navigation.PushModalAsync(new executionTaskView(taskList));
            spinner.IsVisible = false;
            spinner.IsRunning = false;
            ((ListView)sender).SelectedItem = null; 
        }
    }
}
