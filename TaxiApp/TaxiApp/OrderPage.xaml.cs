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
        //Options for the selection of the persons
        int[] personArr = { 1, 2, 3, 4, 5, 6, 7 };
        //Options for the selection of the childseats
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

                //Adding the options to the Pickers
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
        //Button used to retrieve the information, whether or not SharedTaxi Mode is enabled
        private void checkSharedBtn_Clicked(object sender, EventArgs e)
        {
            //REST Service to retrieve the Boolean, telling if SharedTaxi Mode is enabled or not
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://nsterdt.000webhostapp.com/GetSharedTaxiStatus.php");
            request.Method = "GET";
            String content = String.Empty;

            //Get response from the WebRequest
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                content = reader.ReadToEnd();
                reader.Close(); ;
                dataStream.Close();
            }

            //Set the check to be enabled if SharedTaxi mode is enabled, and disable it if not
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

        //Button used to create the Order of the customer
        private async void createOrderBtn_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                //Today's Date, required for the Order in the SQLite database
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

                //REST service to create the order in the MySQL Database
                Uri uri = new Uri("https://nsterdt.000webhostapp.com/CreateOrder.php");
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

                //REST service used to retrieve the ID of the lastly created customer
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://nsterdt.000webhostapp.com/GetHighestOrderID.php");
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