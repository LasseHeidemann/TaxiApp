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
            var content = "";
            var request = WebRequest.Create("http://nsterdt.000webhostapp.com/GetSharedTaxiStatus.php");
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                }
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                    }
                }
            }

        }

        private async void createOrderBtn_ClickedAsync(object sender, EventArgs e)
        {
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

            if (reserveCheck.IsEnabled)
            {
                reservePicker.IsEnabled = true;
                time = reservePicker.Time.ToString("HH:mm");
            } else
            {
                reservePicker.IsEnabled = false;
                time = DateTime.Now.ToString("HH:mm");
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