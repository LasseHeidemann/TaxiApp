using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace TaxiApp
{
    class Database : SQLiteConnection
    {
        public Database(string path) : base(path)
        {
            Initialize();
        }

        void Initialize()
        {
            try
            {
                CreateTable<Customer>();
                CreateTable<Order>();
            } catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        public int RegisterCustomer(Customer customer)
        {
            //Autoincrement the ID of the customer
            int lastID = getNoOfCustomers();
            customer.ID = lastID + 1;
            Console.WriteLine(customer.ToString());

            try
            {
                return Insert(customer);
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public int UpdateCustomerInfo(Customer customer)
        {
            try
            {
                return Update(customer);
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public string GetCustomerName(int id)
        {
            try
            {
                Customer customer = Table<Customer>().Where(c => c.ID == id).First();
                string firstname = customer.FirstName;
                string lastName = customer.LastName;
                return firstname + " " + lastName;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public string GetCustomerEmail(int id)
        {
            try
            {
                Customer customer = Table<Customer>().Where(c => c.ID == id).First();
                return customer.Email;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            try
            {
                Delete(customer);
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public int Login(String mobileNo, String password)
        {
            try
            {
                Customer c = VerifyCustomer(mobileNo, password);
                if (c != null)
                {
                    return c.ID;
                }
                else
                {
                    return 0;
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public Customer VerifyCustomer(String mobileNo, String password)
        {
            try
            {
                return Table<Customer>().Where(c => c.Mobilenumber == mobileNo && c.Password == password).First();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        private int getNoOfCustomers()
        {
            try
            {
                // this counts all customers in the database
                var count = this.ExecuteScalar<int>("SELECT Count(*) FROM Customer");

                return count;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public int CreateOrder(Order order)
        {
            //Autoincrement the ID of the order
            int lastID = getNoOfOrders();
            order.OrderID = lastID + 1;
            Console.WriteLine("lastID: " + lastID);

            try
            {
                Console.WriteLine("hello" + order.ToString());
                return Insert(order);
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        private int getNoOfOrders()
        {
            try
            {
                // this counts all customers in the database
                var count = this.ExecuteScalar<int>("SELECT Count(*) FROM Order");

                return count;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public List<Order> GetOrders(int id)
        {
            return Table<Order>().Where(o => o.CustomerID == id).ToList();
        }
    }
}