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
            catch (SQLiteException)
            {
                return -1;
            }
        }
    }
}