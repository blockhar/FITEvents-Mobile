﻿using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
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
        Button btnDelete;

        User user;

        public UserDetails(User _user)
        {
            user = _user;

            lblUserName = new Label { Text = "Name" };
            lblUserPhone = new Label { Text = "Phone Number" };
            lblUserEmail = new Label { Text = "Email Address (can not change)" };
            entUserName = new Entry { Text = user.userName };
            entUserPhone = new Entry { Text = user.userPhone };
            entUserEmail = new Entry { Text = user.userEmail };
            entUserEmail.IsEnabled = false;
            btnSaveClose = new Button { Text = "Save and Close" };
            btnSaveClose.Clicked += Save;
            btnSaveClose.Clicked += Close;
            btnSave = new Button { Text = "Save" };
            btnSave.Clicked += Save;
            btnDelete = new Button { Text = "Delete" };
            btnDelete.Clicked += OnbtnDeleteClick;

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
                    btnDelete
                }
            };

            this.Content = new ScrollView
            {
                Content = stack
            };

        }

        void Save(object sender, EventArgs e)
        {
            user.userName = entUserName.Text;
            user.userPhone = entUserPhone.Text;
            if (String.IsNullOrEmpty(user.userID))
            {
                user = user.Create();
            }
            else
            {
                user.Save();
            }
        }

        void OnbtnDeleteClick(object sender, EventArgs e)
        {

        }

        void Close(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}