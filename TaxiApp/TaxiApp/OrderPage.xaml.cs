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
	public partial class OrderPage : ContentPage
	{
        int[] personArr = {1, 2, 3, 4};
        int[] childseatsArr = { 0, 1, 2 };
        int id, sharedTaxi, handicapped;
        string persons, childseats;
        String time;

        String destination, location;
		public OrderPage (int ID)
		{
            id = ID;
			InitializeComponent ();

            foreach (int number in personArr)
            {
                personsPicker.Items.Add(number + "");
            }

            foreach (int number in childseatsArr)
            {
                childseatsPicker.Items.Add(number + "");
            }
        }

        private void checkSharedBtn_Clicked(object sender, EventArgs e)
        {

        }

        private async void createOrderBtn_ClickedAsync(object sender, EventArgs e)
        {
            time = DateTime.Now.ToString("HH:mm");

            if (sharedTaxiCheck.IsEnabled)
            {
                sharedTaxi = 1;
            } else
            {
                sharedTaxi = 0;
            }

            if (handicappedCheck.IsEnabled)
            {
                handicapped = 1;
            }else
            {
                handicapped = 0;
            }

            location = locationTxt.Text;
            destination = destinationTxt.Text;
            persons = personsPicker.Items[personsPicker.SelectedIndex];
            childseats = childseatsPicker.Items[childseatsPicker.SelectedIndex];

            Uri uri = new Uri("http://nsterdt.000webhostapp.com/CreateOrder.php");
            WebClient client = new WebClient();
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("CustomerID", id+"");
            parameters.Add("Location", destination);
            parameters.Add("Destination", location);
            parameters.Add("Time", time);
            parameters.Add("SharedTaxi", sharedTaxi+"");
            parameters.Add("Persons", persons);
            parameters.Add("Childseats", childseats);
            parameters.Add("Handicapped", handicapped+"");

            await client.UploadValuesTaskAsync(uri, parameters);
        }

        private void personsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string persons = personsPicker.Items[personsPicker.SelectedIndex];
        }

        private void childseatsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string childseats = childseatsPicker.Items[childseatsPicker.SelectedIndex];
        }
    }
}