using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FITEvents.HomePages;
using FITEvents.Classes;

using Xamarin.Forms;

namespace FITEvents
{
	public partial class App : Application
	{
        public static NavigationPage NavigationPage { get; private set; }

        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new Login());
            //MainPage = new Login();

            Resources = new ResourceDictionary();
            Resources.Add(FITEventStyles.buttonStyle);
            Resources.Add(FITEventStyles.entryStyle);
            Resources.Add(FITEventStyles.labelStyle);
            Resources.Add(FITEventStyles.pickerStyle);
            Resources.Add(FITEventStyles.datePickerStyle);
            Resources.Add(FITEventStyles.timePickerStyle);

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
