using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBConnect
{
    public class Facility
    {
        public int Facility_No { get; set; }
        public string? FacilityName { get; set; }
        public override string ToString()
        {
            return $"ID: {Facility_No} Name: {FacilityName}";
        }
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DemoHotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
    }
}
