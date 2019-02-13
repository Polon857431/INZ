using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pz.Models
{
    public class Spot
    {
        public int ID { get; set; }
        public int EventId { get; set; }
        public int PointId { get; set; }
        public int Value { get; set; }
        public virtual EventModel Event { get; set; }
        public virtual PointModel Point { get; set; }


    }


}

     
