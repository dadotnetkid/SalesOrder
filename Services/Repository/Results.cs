using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Repository
{
    public class Results
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
