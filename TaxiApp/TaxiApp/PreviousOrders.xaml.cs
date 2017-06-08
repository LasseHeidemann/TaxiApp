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
	public partial class PreviousOrders : ContentPage
	{
        //Lists for all orders, previous and upcoming ones
        List<Order> upcomingOrders;
        List<Order> pastOrders;
        List<Order> orderList;
        DateTime now;
        int latestOrderID;
        
        int id;
		public PreviousOrders ()
		{
            try
            {
                //Current time, used to check whether orders are in the past or future
                now = DateTime.Now;
                id = SessionUser.ID;
                InitializeComponent();

                BindingContext = lstPast;
                BindingContext = lstUpcoming;

                orderList = App.DB.GetOrders(id);

                pastOrders = new List<Order>();
                upcomingOrders = new List<Order>();

                foreach (Order o in orderList)
                {
                    //Parsing the strings to a DateTime, in order to compare them
                    DateTime time = DateTime.ParseExact(o.Date + " " + o.Time, "dd-MM-yyyy HH:mm", null);

                    //Add the orders to the upcomingOrders list, if they are in the future, otherwise in the pastOrders list
                    if (now < time)
                    {
                        upcomingOrders.Add(o);
                        latestOrderID = o.OrderID;
                    }
                    else
                    {
                        pastOrders.Add(o);
                    }
                }

                lstPast.ItemsSource = pastOrders;
                lstUpcoming.ItemsSource = upcomingOrders;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Button used to cancel upcoming orders
        private async void cancelOrderBtn_ClickedAsync(object sender, EventArgs e)
        {
            //REST service to set the "isCancelled" parameter in the MatcherOrder table to 1
            Uri uri = new Uri("https://nsterdt.000webhostapp.com/CancelOrder.php");
            WebClient client = new WebClient();
            client.UseDefaultCredentials = true;
            NameValueCollection parameters = new NameValueCollection();

            //Retrieve ID of the latest Order
            parameters.Add("OrderID", latestOrderID + "");

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