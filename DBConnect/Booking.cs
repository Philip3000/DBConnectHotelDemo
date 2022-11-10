using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBConnect
{
    internal class Booking
    {
        public int Booking_No;
        public int Hotel_No;
        public int Guest_No;
        public DateOnly DateFrom;
        public DateOnly DateTo;
        public override string ToString()
        {
            return $"ID: {Booking_No} Hotel: {Hotel_No} Guest: {Guest_No}" +
                $"Booking from: {DateFrom} to: {DateTo}";
        }
    }
}
