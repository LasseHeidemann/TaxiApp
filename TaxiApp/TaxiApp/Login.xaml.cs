﻿using Android.Content;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
    {
        public Login ()
		{
			InitializeComponent ();
		}

        private async void loginBtn_ClickedAsync(object sender, EventArgs e)
        {
            String mobilenumber = loginMobileTxt.Text;
            String password = loginPasswordTxt.Text;
            int id = App.DB.Login(mobilenumber, password);

            if (id != 0)
            {
                SessionUser.ID = id;
                await Navigation.PushAsync(new TabbedMain());
                Navigation.RemovePage(this);
            } else
            {
                await DisplayAlert("Wrong information", "Mobilenumber and password don't match", "OK");
            }
        }

        private async void registerBtn_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
    }
}