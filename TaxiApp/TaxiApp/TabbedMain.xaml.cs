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
    public partial class TabbedMain : TabbedPage
    {
        //The main Page, containing the Order, PreviousOrders, AccountPage and Logout pages
        public TabbedMain ()
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
    }
}