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
	public partial class AccountPage : ContentPage
	{
        int id;
		public AccountPage (int ID)
		{
            id = ID;
            InitializeComponent();
		}

        private void changeEmailBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void changePasswordBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}