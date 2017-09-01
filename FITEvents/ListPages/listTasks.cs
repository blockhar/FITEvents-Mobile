using System;
using System.Collections.Generic;
using System.Linq;
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
        ActivityIndicator spinner;
        List<Task> allTasks;

        public listTasks (Deliverable _deliverable)
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

            deliverable = _deliverable;
            btnNewTask = new Button { Text = "Create New Task" };
            btnNewTask.Clicked += OnbtnNewTaskClick;
            spinner = new ActivityIndicator();

            listView = new ListView
            {
                
                ItemsSource = allTasks,
                HasUnevenRows = true,
                

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "taskName");

                    Label deliverableLabel = new Label();
                    deliverableLabel.SetBinding(Label.TextProperty, "deliverableName");

                    Label dueDateLabel = new Label();
                    dueDateLabel.SetBinding(Label.TextProperty, "dueDateLocal");

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
            listView.SeparatorColor = Color.White;

            Content = new StackLayout {
				Children =
                {
                    btnNewTask,
                    listView,
                    spinner
                }
			};
		}
       
        async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            Task selectedTask = (Task)e.SelectedItem;
            await Navigation.PushModalAsync(new TaskDetails(selectedTask, deliverable.team));

            ((ListView)sender).SelectedItem = null; 
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }

        private async void OnbtnNewTaskClick(object sender, EventArgs e)
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            Task newtask = new Task();
            newtask.deliverableID = deliverable.deliverableID;
            newtask.deliverableName = deliverable.deliverableName;
            await Navigation.PushModalAsync(new TaskDetails(newtask, deliverable.team));
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }

        async override protected void OnAppearing()
        {
            spinner.IsRunning = true;
            spinner.IsVisible = true;
            allTasks = await Task.GetAllTasks(deliverable.deliverableID);
            listView.ItemsSource = allTasks;
            spinner.IsRunning = false;
            spinner.IsVisible = false;
        }
    }
}
