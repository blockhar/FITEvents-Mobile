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
	public class listTasks : ContentPage
	{
        ListView listView;
        Button btnNewTask;
        Deliverable deliverable;

        public listTasks (List<Task> allTasks, Deliverable _deliverable)
		{
            deliverable = _deliverable;
            btnNewTask = new Button { Text = "Create New Task" };
            btnNewTask.Clicked += OnbtnNewTaskClick;

            listView = new ListView
            {
                
                // Source of data items.
                ItemsSource = allTasks,
                HasUnevenRows = true,
                

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "taskName");

                    Label deliverableLabel = new Label();
                    deliverableLabel.SetBinding(Label.TextProperty, "deliverableName");

                    Label dueDateLabel = new Label();
                    dueDateLabel.SetBinding(Label.TextProperty, "dueDate");

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
                                deliverableLabel,
                                dueDateLabel                              
                            }
                        }
                    };
                })
            };

            listView.ItemSelected += OnSelection;

            Content = new StackLayout {
				Children =
                {
                    btnNewTask,
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

            Task selectedTask = (Task)e.SelectedItem;
            Navigation.PushModalAsync(new TaskDetails(selectedTask));

            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }

        private void OnbtnNewTaskClick(object sender, EventArgs e)
        {
            Task newtask = new Task();
            newtask.deliverableID = deliverable.deliverableID;
            newtask.deliverableName = deliverable.deliverableName;
            Navigation.PushModalAsync(new TaskDetails(newtask));
        }
    }
}
