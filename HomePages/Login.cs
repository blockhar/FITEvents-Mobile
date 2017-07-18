
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using RestSharp;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FITEvents.Classes;
using FITEvents.ItemPages;
using FITEvents.ListPages;
using FITEvents.ExecutionPages;
using Android.Util;

namespace FITEvents.HomePages
{
    public class Login : ContentPage
    {

        StackLayout LoginLayout = new StackLayout();
        Label lblUsername;
        Entry entUsername;
        Label lblPassword;
        Entry entPassword;
        Button btnLogin;
        Button btnSignup;

        public Login()
        {
            lblUsername = new Xamarin.Forms.Label
            {
                Text = "Username"
            };

            entUsername = new Entry
            {
                Placeholder = "Username"
            };

            entPassword = new Entry
            {
                Placeholder = "Please Enter Password"
            };
            entPassword.IsPassword = true;

            lblPassword = new Xamarin.Forms.Label
            {
                Text = "Password"
            };

            btnLogin = new Button
            {
                Text = "Login!"
            };
            btnLogin.Clicked += OnLoginBtnClick;

            btnSignup = new Button
            {
                Text = "Signup (and log in)"
            };
            btnSignup.Clicked += OnSignupBtnClick;


            this.Content = new StackLayout
            {
                Children = {
                    lblUsername,
                    entUsername,
                    lblPassword,
                    entPassword,
                    btnLogin,
                    btnSignup
                }
            };
        }

        void OnSignupBtnClick(object sender, EventArgs args)
        {
            string username = entUsername.Text.ToLower();
            string password = entPassword.Text;

            var client = new RestClient("https://forecheckit.auth0.com");
            var request = new RestRequest("dbconnections/signup", Method.POST);

            request.AddParameter("client_id", Globals.clientID);
            request.AddParameter("email", username);
            request.AddParameter("password", password);
            request.AddParameter("connection", "Username-Password-Authentication");

            IRestResponse response = client.Execute(request);

            //lblResponse.Text = response.Content.ToString();
            Log.Info("FITEVENTS", "Successful Login");

            if ((int)response.StatusCode == 200)
            {
                Log.Info("FITEVENTS","Successful signup");
                if (TryLogin())
                {

                    User user = new User();
                    user.userEmail = username;
                    user.Create();
                    Log.Info("FITEVENTS", "User Created");
                    Globals.loggedInUser = User.GetLoggedInUser();
                    Navigation.PushModalAsync(new Home());
                }
                else
                {
                    Log.Info("FITEVENTS", "Fail Login");
                    DisplayAlert("Error", "Error. Http Status: " + response.StatusCode + ". Error Message: " + response.Content, "OK");
                }
                
            }

            else
            {
                Log.Info("FITEVENTS", "Fail Signup");
                DisplayAlert("Error", "Error. Http Status: " + response.StatusCode + ". Error Message: " + response.Content, "OK");
            }
               

        }

        void OnLoginBtnClick(object sender, EventArgs args)
        {
            TryLogin();
            Globals.loggedInUser = User.GetLoggedInUser();
            Navigation.PushModalAsync(new Home());
        }

        bool TryLogin()
        {
            string username = entUsername.Text.ToLower();
            string password = entPassword.Text;

            var client = new RestClient("https://forecheckit.auth0.com");
            var request = new RestRequest("oauth/ro", Method.POST);

            request.AddParameter("client_id", Globals.clientID);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("connection", "Username-Password-Authentication");
            request.AddParameter("grant_type", "password");
            request.AddParameter("scope", "openid");

            IRestResponse response = client.Execute(request);

            if ((int)response.StatusCode == 200)
            {
                Log.Info("FITEVENTS", "Login Success");
                string responseText = response.Content;
                JObject responeObject = JObject.Parse(responseText);
                string id_token = (string)responeObject["id_token"];
                Globals.BearerCode = id_token;
                Log.Info("FITEVENTS", "Try Get LoggedInUser");                
                return true;
                
            }

            else
            {
                DisplayAlert("Error", "Error. Http Status: " + response.StatusCode + ". Error Message: " + response.Content, "OK");
                return false;
            }
        }
    }
}