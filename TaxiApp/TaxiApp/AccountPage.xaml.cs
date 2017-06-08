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
	public partial class AccountPage : ContentPage
	{
        int id;
        string name, oldEmail, newEmail, oldPassword, newPassword, newPasswordRepeat, email;
		public AccountPage ()
		{
            try
            {
                //Get the ID of the SessionUser
                id = SessionUser.ID;
                name = App.DB.GetCustomerName(id);
                InitializeComponent();
                this.Title = name;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Button used to change the customers email address, comparing his old one and the entered new one.
        private async void changeEmailBtn_ClickedAsync(object sender, EventArgs e)
        {
            oldEmail = oldEmailTxt.Text;
            newEmail = newEmailTxt.Text;

            if (!oldEmail.Equals(newEmail))
            {
                //Calling the REST service to update the email adress
                Uri uri = new Uri("https://nsterdt.000webhostapp.com/UpdateEmail.php");
                WebClient client = new WebClient();
                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("OldEmail", oldEmail);
                parameters.Add("NewEmail", newEmail);


                try
                {
                    await client.UploadValuesTaskAsync(uri, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                await DisplayAlert("Try Again.", "The specified emails are the same.", "OK");
            }
        }

        //Button used to change the customers password, using his old one and comparing it to the new
        private async void changePasswordBtn_ClickedAsync(object sender, EventArgs e)
        {
            oldPassword = oldPasswordTxt.Text;
            newPassword = newPasswordTxt.Text;
            newPasswordRepeat = newPasswordRepeatTxt.Text;
            email = App.DB.GetCustomerEmail(id);

            if (!oldPassword.Equals(newPassword) && newPassword.Equals(newPasswordRepeat))
            {
                //Calling the REST service to update the password
                Uri uri = new Uri("https://nsterdt.000webhostapp.com/UpdatePassword.php");
                WebClient client = new WebClient();
                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("NewPassword", newPassword);
                parameters.Add("Email", email);

                try
                {
                    await client.UploadValuesTaskAsync(uri, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                await DisplayAlert("Try Again.", "The specified passwords don't match, or are not new.", "OK");
            }
        }
    }
}