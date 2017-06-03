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
        [PrimaryKey, AutoIncrement]
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public DateTime Time { get; set; }
        public Boolean SharedTaxi { get; set; }
        public int NoOfPersons { get; set; }
        public int Childseats { get; set; }
        public Boolean Handicapped { get; set; }


        private int customerId, noOfPersons, childSeats;
        private string location, destination;
        private DateTime time;
        private Boolean sharedTaxi, handicapped;

        public Order(int custID, string loc, string dest, DateTime dateTime, Boolean shared, int persons, int children, Boolean handicap)
        {
            customerId = custID;
            location = loc;
            destination = dest;
            time = dateTime;
            sharedTaxi = shared;
            noOfPersons = persons;
            childSeats = children;
            handicapped = handicap;
        }

        public override string ToString()
        {
            return string.Format("[Order: OrderID={0}, CustomerID={1}, Location={2}, Destination={3}," +
                "Time={4}, SharedTaxi={5}, NoOfPersons={6}, Childseats={7}, Handicapped={8}]",
                OrderID, CustomerID, Location, Destination, Time, SharedTaxi, NoOfPersons, Childseats, Handicapped);
        }
    }
}