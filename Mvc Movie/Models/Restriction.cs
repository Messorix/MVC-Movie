using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_Movie.Classes
{
    public class Restriction
    {
        public int ID { get; set; }
        public string ISO_3166_1 { get; set; }
        public string Certification { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }
}