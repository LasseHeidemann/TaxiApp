using Android.Content;
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
	public partial class Register : ContentPage
	{
        String fName, lName, email, mobileNumber, password;
        public Register ()
		{
			InitializeComponent ();
            //DB = new Database();
		}

        private async void CreateAccBtn_ClickedAsync(object sender, EventArgs e)
        {
            fName = firstNameTxt.Text;
            lName = lastNameTxt.Text;
            email = emailTxt.Text;
            mobileNumber = mobileNumberTxt.Text;
            password = passwordTxt.Text;

            Customer c = new Customer();
            c.FirstName = fName;
            c.LastName = lName;
            c.Email = email;
            c.Mobilenumber = mobileNumber;
            c.Password = password;

            App.DB.RegisterCustomer(c);

            Uri uri = new Uri("http://nsterdt.000webhostapp.com/CreateCustomer.php");
            WebClient client = new WebClient();
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("FirstName", fName);
            parameters.Add("LastName", lName);
            parameters.Add("Email", email);
            parameters.Add("MobileNumber", mobileNumber);
            parameters.Add("Password", password);

            await client.UploadValuesTaskAsync(uri, parameters);
        }

    }
}