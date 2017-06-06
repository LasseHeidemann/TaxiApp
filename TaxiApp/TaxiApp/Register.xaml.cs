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

            Uri uri = new Uri("https://divided-cages.000webhostapp.com/CreateCustomer.php");
            WebClient client = new WebClient();
            client.UseDefaultCredentials = true;
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("FirstName", fName);
            parameters.Add("LastName", lName);
            parameters.Add("Email", email);
            parameters.Add("MobileNumber", mobileNumber);
            parameters.Add("Password", password);

            try
            {
                await client.UploadValuesTaskAsync(uri, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}