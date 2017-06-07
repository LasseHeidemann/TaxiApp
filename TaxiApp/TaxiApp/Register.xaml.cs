using Android.Content;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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

            //Get highest customer ID from MySQL
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://divided-cages.000webhostapp.com/GetHighestCustomerID.php");
            request.Method = "GET";
            String highestID = String.Empty;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                highestID = reader.ReadToEnd();
                reader.Close(); ;
                dataStream.Close();
            }

            Customer c = new Customer();

            Int32.TryParse(highestID, out int costumerID);
            c.ID = costumerID;
            c.FirstName = fName;
            c.LastName = lName;
            c.Email = email;
            c.Mobilenumber = mobileNumber;
            c.Password = password;

            App.DB.RegisterCustomer(c);

            await Navigation.PopAsync();
        }

    }
}