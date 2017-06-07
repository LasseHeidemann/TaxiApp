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
                    DateTime time = DateTime.ParseExact(o.Date + " " + o.Time, "dd-MM-yyyy HH:mm", null);
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

        private async void cancelOrderBtn_ClickedAsync(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://divided-cages.000webhostapp.com/CancelOrder.php");
            WebClient client = new WebClient();
            client.UseDefaultCredentials = true;
            NameValueCollection parameters = new NameValueCollection();

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