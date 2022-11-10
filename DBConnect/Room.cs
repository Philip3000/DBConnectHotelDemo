using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBConnect
{
    public class Room
    {
        public char? Types { get; set; }
        public float Price { get; set; }
        public override string ToString()
        {
            return $"Type: {Types} Price {Price}";
        }
    }
}
