using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
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
            //Cache.taskList = Task.GetAllTasks("delID123");
            

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
            // Create the ListView.
            ListView listView = new ListView
            {
                // Source of data items.

                ItemsSource = groupedWithTitleProperty,
                HasUnevenRows = true,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    //Label nameLabel = new Label();
                    //nameLabel.SetBinding(Label.TextProperty, "name");
                    //nameLabel.SetBinding(Label.HorizontalOptionsProperty, "Hor");

                    Label dueLabel = new Label();
                    dueLabel.SetBinding(Label.TextProperty, "dueDateGroup");

                    //Label dueGroupLabel = new Label();
                    //dueGroupLabel.SetBinding(Label.TextProperty, "dueDateGroup", BindingMode.Default, null, stringFormat: "Due Group: {0:}");

                    //Label assignedToLabel = new Label();
                    //assignedToLabel.SetBinding(Label.TextProperty, "assignedToName", BindingMode.Default, null, stringFormat: "Assigned To: {0:}");

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


                    //myView.SetBinding(BackgroundColorProperty, "bgColor");

                    // Return an assembled ViewCell.
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
            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }
    }
}
