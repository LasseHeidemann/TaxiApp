using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiApp
{
    class SessionUser
    {
        int id;
        public SessionUser(int ID)
        {
            id = ID;
        }

        public int ID { get; set; }
    }
}
