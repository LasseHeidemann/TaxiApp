using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TaxiApp
{
	public partial class App : Application
	{
        internal static Database DB;
        public App ()
		{
			InitializeComponent();

            string dbFile = "CustomerDB.db3";
            var dbPath = "";

#if __ANDROID__
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dbPath = System.IO.Path.Combine(docPath, dbFile);
            Console.WriteLine(dbPath);
#else
#if __IOS__
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string libPath = System.IO.Path.Combine(docPath, "..", "Library");
        dbPath = System.IO.Path.Combine(libPath, dbFile);
#else
#if WINDOWS_UWP
          dbPath = System.IO.Path.Combine( Windows.Storage.ApplicationData.Current.LocalFolder.Path, dbFile);
        //dbPath = System.IO.Path.Combine( ApplicationData.Current.LocalFolder.Path, dbFile);
#endif
#endif
#endif

            DB = new Database(dbPath);

            MainPage = new NavigationPage(new Login());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
