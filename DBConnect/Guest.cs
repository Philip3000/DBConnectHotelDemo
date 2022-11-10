using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect
{
    internal class Guest
    {
       public int Guest_No { get; set; }
       public string? Name { get; set; }
       public string? Address { get; set; }
       public override string ToString()
       {
           return $"ID: {Guest_No} Name: {Name} Address {Address}";
       }
    }
}
