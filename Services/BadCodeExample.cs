using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Services
{
    public class BadCodeExample
    {
        public void ProcessBooking(string customerName, int age, double price)
        {
            string unusedVariable = "test";

            if (customerName != null)
            {
                if (customerName != "")
                {
                    if (age > 18)
                    {
                        if (price > 100)
                        {
                            double discount = price * 0.1;
                            double total = price - discount;

                            Console.WriteLine("Customer: " + customerName);
                            Console.WriteLine("Total: " + total);

                            try
                            {
                                if (total > 0)
                                {
                                    Console.WriteLine("Booking confirmed");
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }

            if (age > 18)
            {
                Console.WriteLine("Adult customer");
            }

            if (age > 18)
            {
                Console.WriteLine("Eligible customer");
            }
        }

        public double CalculateDiscount(double price)
        {
            if (price > 100)
                return price * 0.1;

            if (price > 100)
                return price * 0.1;

            return 0;
        }
    }
}
