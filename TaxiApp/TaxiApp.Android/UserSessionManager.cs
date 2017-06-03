using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TaxiApp.Droid
{
    class UserSessionManager
    {
        ISharedPreferences pref;
        ISharedPreferencesEditor editor;
        Context _context;
        private const String pref_name = "AndroidExamplePref";
        private const String isLoggedIn = "IsUserLoggedIn";
        public const String key_mobileNo = "mobileNumber";
        public const String key_pass = "password";

        public UserSessionManager(Context context)
        {
            this._context = context;
            pref = _context.GetSharedPreferences(pref_name, 0);
            editor = pref.Edit();
        }

        //Create login session
        public void createUserLoginSession(String name, String email)
        {
            // Storing login value as TRUE
            editor.PutBoolean(isLoggedIn, true);

            // Storing name in pref
            editor.PutString(key_mobileNo, name);

            // Storing email in pref
            editor.PutString(key_pass, email);

            // commit changes
            editor.Commit();
        }

        //Checks if the user is logged in. If not, redirects him to the the login page. If he's logged in he get's
        //directed to the main page.
        public Boolean checkLogin()
        {
            // Check login status
            if (!this.isUserLoggedIn())
            {
                

                return true;
            }
            return false;
        }

        // Check for login
        public Boolean isUserLoggedIn()
        {
            return pref.GetBoolean(isLoggedIn, false);
        }
    }
}