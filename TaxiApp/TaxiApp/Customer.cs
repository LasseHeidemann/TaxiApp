using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiApp
{
    [Table("Customer")]
    class Customer
    {
        [PrimaryKey]
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Mobilenumber { get; set; }
        public String Password { get; set; }

        public override string ToString()
        {
            return string.Format("[Customer: ID={0}, FirstName={1}, LastName={2}, Email={3}, Mobilenumber={4}, Password={5}]", 
                ID, FirstName, LastName, Email, Mobilenumber, Password);
        }
    }
}
