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
	public partial class PreviousOrders : ContentPage
	{
        List<Order> orderList;
        int id;
		public PreviousOrders ()
		{
            id = SessionUser.ID;
            InitializeComponent();
            this.BindingContext = orderList;
            
            orderList = App.DB.GetOrders(id);
            lstOrders.ItemsSource = orderList;

            foreach (Order o in orderList)
            {
                Console.WriteLine(o.ToString());
            }
		}
	}
}