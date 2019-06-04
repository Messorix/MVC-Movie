using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mvc_Movie.Classes
{
    public class Certifier
    {
        public int ID { get; set; }
        public virtual Restriction Certification { get; set; }
        public virtual Movie ParentMovie { get; set; }
    }
}