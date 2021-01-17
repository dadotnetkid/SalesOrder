using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Customers
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => this.FirstName + " " + this.LastName;
    }
}
