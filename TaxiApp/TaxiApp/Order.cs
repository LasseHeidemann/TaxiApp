using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace TaxiApp
{
    [Table("Order")]
    class Order
    {
        [PrimaryKey]
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public Boolean SharedTaxi { get; set; }
        public string NoOfPersons { get; set; }
        public string Childseats { get; set; }
        public Boolean Handicapped { get; set; }

        public override string ToString()
        {
            return string.Format("[Order: OrderID={0}, CustomerID={1}, Location={2}, Destination={3}, " +
                "Date={4}, Time={5}, SharedTaxi={6}, NoOfPersons={7}, Childseats={8}, Handicapped={9}]",
                OrderID, CustomerID, Location, Destination, Date, Time, SharedTaxi, NoOfPersons, Childseats, Handicapped);
        }
    }
}