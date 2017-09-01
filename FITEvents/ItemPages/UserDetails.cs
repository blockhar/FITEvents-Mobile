using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.Classes;
using FITEvents.ModalPages;
using Xamarin.Forms;
using Android.Util;
namespace FITEvents.ItemPages
{
    class UserDetails : ContentPage
    {
        Label lblUserName;
        Label lblUserPhone;
        Label lblUserEmail;
        Entry entUserName;
        Entry entUserPhone;
        Entry entUserEmail;
        Button btnSave;
        Button btnSaveClose;
        //Button btnDelete;
        ActivityIndicator spinner;

        User user;

        public UserDetails(User _user)
        {
            this.BackgroundImage = FITEventStyles.GlobalBGImageName;

            user = _user;

            lblUserName = new Label { Text = "Name" };
            lblUserPhone = new Label { Text = "Phone Number" };
            lblUserEmail = new Label { Text = "Email Address (can not change)" };
            entUserName = new Entry { Text = user.userName };
            entUserPhone = new Entry { Text = user.userPhone };
            entUserEmail = new Entry { Text = user.userEmail };
            entUserEmail.InputTransparent = true; //prevents user from changing. Using IsEnabled = false causes an error where the text color cannot be changed from the default black
            btnSaveClose = new Button { Text = "Save and Close" };
            btnSaveClose.Clicked += Save;
            btnSaveClose.Clicked += Close;
            btnSave = new Button { Text = "Save" };
            btnSave.Clicked += Save;
            //btnDelete = new Button { Text = "Delete" };
            //btnDelete.Clicked += OnbtnDeleteClick;
            spinner = new ActivityIndicator();

            StackLayout stack = new StackLayout
            {
                Children = {
                    lblUserName,
                    entUserName,
                    lblUserPhone,
                    entUserPhone,
                    lblUserEmail,
                    entUserEmail,
                    btnSaveClose,
                    btnSave,
                    //btnDelete,
                    spinner
                }
            };

            this.Content = new ScrollView
            {
                Content = stack
            };

        }

        async void Save(object sender, EventArgs e)
        {
            spinner.IsVisible = true;
            spinner.IsRunning = true;
            user.userName = entUserName.Text;
            user.userPhone = entUserPhone.Text;
            if (String.IsNullOrEmpty(user.userID))
            {
                user = await user.Create();
            }
            else
            {
                user.Save();
            }
            spinner.IsVisible = false;
            spinner.IsRunning = false;
        }

        //void OnbtnDeleteClick(object sender, EventArgs e)
        //{
        //    spinner.IsVisible = true;
        //    spinner.IsRunning = true;
        //    user.Delete();
        //    Navigation.PopModalAsync();
        //    spinner.IsVisible = false;
        //    spinner.IsRunning = false;
        //}

        void Close(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}