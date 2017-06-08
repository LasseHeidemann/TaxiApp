using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Logout : ContentPage
	{
		public Logout ()
		{
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Button used to log the customers account out
        private void logoutBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Reset the SessionUser ID and redirect the customer to the Login Page
                SessionUser.ID = 0;
                Application.Current.MainPage = new NavigationPage(new Login());
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}