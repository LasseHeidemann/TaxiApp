using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderPage : ContentPage
	{
        int[] personArr = { 1, 2, 3, 4 };
        int[] childseatsArr = { 0, 1, 2 };
        int sharedTaxi, handicapped, id;
        string persons, childseats;
        String time, date;

        String destination, location;

       
        public OrderPage ()
		{
            try
            {
                id = SessionUser.ID;
                InitializeComponent();

                foreach (int number in personArr)
                {
                    personsPicker.Items.Add(number + "");
                }

                foreach (int number in childseatsArr)
                {
                    childseatsPicker.Items.Add(number + "");
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
}

        private void checkSharedBtn_Clicked(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://divided-cages.000webhostapp.com/GetSharedTaxiStatus.php");
            request.Method = "GET";
            String content = String.Empty;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                content = reader.ReadToEnd();
                reader.Close(); ;
                dataStream.Close();
            }

            if (content == "1")
            {
                sharedTaxiCheck.IsEnabled = true;
            } 
            else if (content == "0")
            {
                sharedTaxiCheck.IsEnabled = false;
                sharedTaxiCheck.IsToggled = false;
            }

        }

        private async void createOrderBtn_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                date = DateTime.Now.ToString("dd/MM/yyyy");
                if (sharedTaxiCheck.IsToggled)
                {
                    sharedTaxi = 1;
                }
                else
                {
                    sharedTaxi = 0;
                }

                if (handicappedCheck.IsToggled)
                {
                    handicapped = 1;
                }
                else
                {
                    handicapped = 0;
                }

                if (reserveCheck.IsToggled)
                {
                    reservePicker.IsEnabled = true;
                    time = reservePicker.Time.ToString("h':'mm");
                }
                else
                {
                    reservePicker.IsEnabled = false;
                    time = DateTime.Now.ToString("HH:mm");
                }

                location = locationTxt.Text;
                destination = destinationTxt.Text;
                persons = personsPicker.Items[personsPicker.SelectedIndex];
                childseats = childseatsPicker.Items[childseatsPicker.SelectedIndex];

                Uri uri = new Uri("https://divided-cages.000webhostapp.com/CreateOrder.php");
                WebClient client = new WebClient();
                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("CustomerID", id + "");
                parameters.Add("Location", location);
                parameters.Add("Destination", destination);
                parameters.Add("Time", time);
                parameters.Add("SharedTaxi", sharedTaxi + "");
                parameters.Add("Persons", persons);
                parameters.Add("Childseats", childseats);
                parameters.Add("Handicapped", handicapped + "");


                try
                {
                    await client.UploadValuesTaskAsync(uri, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                //Get highest order ID
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://divided-cages.000webhostapp.com/GetHighestOrderID.php");
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


                Order o = new Order();

                Int32.TryParse(highestID, out int orderID);
                o.OrderID = orderID;
                o.CustomerID = id;
                o.Location = location;
                o.Destination = destination;
                o.Date = date;
                o.Time = time;
                if (sharedTaxi == 0)
                {
                    o.SharedTaxi = false;
                }
                else
                {
                    o.SharedTaxi = true;
                }

                o.NoOfPersons = persons;
                o.Childseats = childseats;
                if (handicapped == 0)
                {
                    o.Handicapped = false;
                }
                else
                {
                    o.Handicapped = true;
                }

                Console.WriteLine(o.ToString());

                App.DB.CreateOrder(o);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void personsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string persons = personsPicker.Items[personsPicker.SelectedIndex];
        }

        private void childseatsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string childseats = childseatsPicker.Items[childseatsPicker.SelectedIndex];
        }

        private void reserveCheck_Toggled(object sender, ToggledEventArgs e)
        {
            if (reserveCheck.IsToggled){
                reservePicker.IsEnabled = true;
            } else
            {
                reservePicker.IsEnabled = false;
            }
        }

    }
}